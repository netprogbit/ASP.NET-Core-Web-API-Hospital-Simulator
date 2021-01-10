using DataLayer.Entities;
using DataLayer.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Server.DTOs;
using Server.Hubs;
using Server.Settings;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Services
{
    /// <summary>
    /// Mqtt hosted service
    /// </summary>
    public class MqttHostedService : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private readonly IHubContext<HospitalHub> _hospitalHub;
        private readonly IServiceProvider _serviceProvider;

        public MqttHostedService(IOptions<AppSettings> appSettings, IHubContext<HospitalHub> hospitalHub, IServiceProvider serviceProvider)
        {
            _appSettings = appSettings.Value;
            _hospitalHub = hospitalHub;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()                
                .WithTcpServer(_appSettings.MqttHost, _appSettings.MqttPort)
                .WithClientId(_appSettings.MqttClientId)             
                .Build();            
            
            // Mqtt message received handler
            mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                string jsonMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Debug.WriteLine($"Message: {jsonMessage}");
                MessageDto messageDto = JsonSerializer.Deserialize<MessageDto>(jsonMessage);
                int deviceId = await UpdateMeasurementDbAsync(messageDto);

                if (deviceId > 0)
                    await _hospitalHub.Clients.All.SendAsync(deviceId.ToString(), jsonMessage); // Send realtime message to frontend`                  
            });

            // Mqtt connected handler
            mqttClient.UseConnectedHandler(async e =>
            {
                await mqttClient.SubscribeAsync(_appSettings.MqttTopic, MqttQualityOfServiceLevel.AtLeastOnce);
            });

            // Mqtt disconnected handler
            mqttClient.UseDisconnectedHandler(async e =>
            {
                await Task.Delay(_appSettings.MqttReconnectDelayMs);
                await mqttClient.ConnectAsync(options);
            });

            await mqttClient.ConnectAsync(options);
        } 
        
        private async Task<int> UpdateMeasurementDbAsync(MessageDto messageDto)
        {
            Device currDevice = null;

            using (var scope = _serviceProvider.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>(); // Geting unit of work service

                // Update measurement DB transaction
                using (var dbContextTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        currDevice = await _unitOfWork.Devices.FindAsync(d => d.SerialNumber == messageDto.SerialNumber);

                        if (currDevice == null)
                            return 0;

                        // Set new buffer current position 

                        currDevice.BufferCurrentPtr++;

                        if (currDevice.BufferCurrentPtr >= currDevice.BufferStartPtr + _appSettings.NumberOfMeasurementInBuffer)
                        {
                            currDevice.BufferCurrentPtr = currDevice.BufferStartPtr;
                        }                        

                        // Updating measurement

                        Measurement currMeasurement = await _unitOfWork.Measurements.FindAsync(m => m.Id == currDevice.BufferCurrentPtr);

                        if (currMeasurement == null)
                            return 0;

                        currMeasurement.HR = messageDto.HR;
                        currMeasurement.RR = messageDto.RR;
                        _unitOfWork.Measurements.Update(currMeasurement);                        
                        _unitOfWork.Devices.Update(currDevice);
                        await _unitOfWork.SaveAsync();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback(); // Rollbacking DB      
                        ExceptionDispatchInfo.Capture(ex).Throw();
                    }
                }
            }

            return currDevice.Id;
        }
    }
}
