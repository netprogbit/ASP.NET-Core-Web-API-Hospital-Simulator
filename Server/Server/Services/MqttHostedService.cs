using DataLayer.Entities;
using DataLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Server.DTOs;
using Server.Hubs;
using Server.Settings;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Services
{
    public class MqttHostedService : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private readonly IHubContext<HospitalHub> _hospitalHub;

        public MqttHostedService(IOptions<AppSettings> appSettings, IHubContext<HospitalHub> hospitalHub)
        {
            _appSettings = appSettings.Value;
            _hospitalHub = hospitalHub;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                            .WithTcpServer(_appSettings.BrokerHost, _appSettings.BrokerPort)
                            .Build();            

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Debug.WriteLine($"Message: {message}");
                MessageDto messageDto = JsonSerializer.Deserialize<MessageDto>(message);
                
                //...
            });

            mqttClient.UseConnectedHandler(async e =>
            {
                await mqttClient.SubscribeAsync(_appSettings.BrokerTopic, MqttQualityOfServiceLevel.AtLeastOnce);
            });

            await mqttClient.ConnectAsync(options);
        }        
    }
}
