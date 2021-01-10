using DeviceSimulator.DTOs;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceSimulator
{
    class Program
    {        
        private static readonly string _host = "broker.hivemq.com";
        private static readonly int _port = 1883;
        private static readonly string _clientId = "yuriipubli";
        private static readonly string _topic = "/vitalerter/yurii/vitals";
        private static readonly string _serialNumber = "12345";
        private static readonly int _publishPeriodSec = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulation...");
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            _ = RunMqttClientAsync(mqttClient);
            Console.WriteLine("Press any key to stop");
            string s = Console.ReadLine();
            _ = StopMqttClientAsync(mqttClient);
        }

        public static async Task RunMqttClientAsync(IMqttClient mqttClient)
        {
            try
            {
                var options = new MqttClientOptionsBuilder()                                                                                    
                    .WithTcpServer(_host, _port)
                    .WithClientId(_clientId)
                    .Build();

                await mqttClient.ConnectAsync(options);

                // Publishing message

                Random rng = new Random();
                
                while (true)
                {
                    int hr = rng.Next(50, 121);
                    int rr = rng.Next(6, 31);

                    MessageDto messageDto = new MessageDto { SerialNumber = _serialNumber, HR = hr, RR = rr };

                    string jsonMessage = JsonSerializer.Serialize<MessageDto>(messageDto);

                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(_topic)
                        .WithPayload(jsonMessage)
                        .WithAtLeastOnceQoS()
                        .WithRetainFlag()
                        .Build();

                    await mqttClient.PublishAsync(message);
                    await Task.Delay(new TimeSpan(0, 0, _publishPeriodSec));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task StopMqttClientAsync(IMqttClient mqttClient)
        {
            try
            {
                await mqttClient.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
