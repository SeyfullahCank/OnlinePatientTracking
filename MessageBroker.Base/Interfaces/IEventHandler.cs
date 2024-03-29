using MessageBroker.Base.Events;

namespace MessageBroker.Base.Abstract
{
    public interface IEventHandler<TIntegrationEvent> : IntegrationEventHandler where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);


    }

    public interface IntegrationEventHandler
    {

    }
}
