namespace Server.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; } // Authentication Key
        public string UrlOrigin { get; set; } // Server URL
        public string UrlClient { get; set; } // Client URL
        public string BrokerHost { get; set; } // MQTT broker host
        public int BrokerPort { get; set; } // MQTT broker port
        public string BrokerClientId { get; set; } // MQTT broker ClientId
        public string BrokerTopic { get; set; } // MQTT broker topic
    }
}
