using MessageBroker.Base.Events;

namespace Notification.API.Events
{
    public class PatientCreatedEvent : IntegrationEvent
    {
        public Guid PatientId { get; private set; }
        public string FullName { get; private set; }
    }
}
