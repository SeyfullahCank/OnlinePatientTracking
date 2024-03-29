namespace MessageBroker.Base
{
    /// <summary>
    /// EventBusConfig'in  amacı:
    /// EventBus'ı kullanmak isteyen neyi kullanacak(RabbitMq, AzureServiceBus vs), bunu kullanırken connection için gerekli bilgileri vs alma ihtiyacı var.
    /// WarehouseCreatedEvent oluşturduğumuzda bu kuyruğu hangi uygulamaların dinlediğinin bilgisi SubscriberClientAppName'den gelmiş olacak.
    /// </summary>
    public class MessageBrokerConfiguration
    {
        public int ConnectionRetryCount { get; set; } = 5;
        public string DefaultTopicName { get; set; } = "mm";
        public string EventBusConnectionString { get; set; } = String.Empty;

        /// <summary>
        /// Kuyruğu oluşturacak olan application'ın ismi.
        /// </summary>
        public string SubscriberClientAppName { get; set; } = String.Empty;
        public string EventNamePrefix { get; set; } = String.Empty;
        public string EventNameSuffix { get; set; } = "Event";
        public MessageBrokerTypes MessageBrokerType { get; set; } = MessageBrokerTypes.RabbitMQ;
        

        
        /// <summary>
        /// object olmasının sebebi : bu base projesini reference alan diğer projelerin buraya eklenecek olan kütüphaneleri eklemek zorunda kalmamalarını sağlamak. Yani RabbitMQ ConnectionFactory yazsaydık buraya, EventBusBase'i kullanan her micro service RabbitMQ'yu kurmak zorunda kalırdı.
        /// </summary>
        public object Connection { get; set; }

        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);
        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);
    }

    public enum MessageBrokerTypes
    {
        RabbitMQ = 0,
        AzureServiceBus = 1
    }
}
