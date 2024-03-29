using MessageBroker.Base.Events;

namespace MessageBroker.Base.Abstract
{
    public interface IMessageBroker
    {
        void Publish(IntegrationEvent @event);
        void Publish(IntegrationEvent @event, string exchangeName);

        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IEventHandler<T>;
        void Subscribe<T, TH>(string exchangeName) where T : IntegrationEvent where TH : IEventHandler<T>;
        void Subscribe<T, TH>(string exchangeName, string queueName) where T : IntegrationEvent where TH : IEventHandler<T>;

        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IEventHandler<T>;
    }
}
