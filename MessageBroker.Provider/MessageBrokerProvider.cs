using Ate.MM.EventBus.RabbitMQ;
using MessageBroker.Base;
using MessageBroker.Base.Abstract;

namespace MessageBroker.Provider
{
    public class MessageBrokerProvider
    {
        public static IMessageBroker Provide(MessageBrokerConfiguration config, IServiceProvider serviceProvider)
        {
            return config.MessageBrokerType switch
            {
                MessageBrokerTypes.RabbitMQ => new MessageBrokerRabbitMQ(config, serviceProvider),
                MessageBrokerTypes.AzureServiceBus => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
