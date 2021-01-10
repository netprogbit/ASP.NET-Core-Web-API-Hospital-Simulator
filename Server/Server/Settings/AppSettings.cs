namespace Server.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; } // Authentication Key
        public string UrlOrigin { get; set; } // Server URL
        public string UrlClient { get; set; } // Client URL
        public string MqttHost { get; set; } // MQTT broker host
        public int MqttPort { get; set; } // MQTT broker port
        public string MqttClientId { get; set; } // MQTT broker ClientId
        public string MqttTopic { get; set; } // MQTT broker topic
        public int MqttReconnectDelayMs { get; set; } // MQTT broker reconnect delay millisecons
        public int NumberOfMeasurementInBuffer { get; set; } // Number of measurement in the buffer
    }
}
