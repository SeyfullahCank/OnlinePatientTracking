using MessageBroker.Base.Events;

namespace Notification.API.Events
{
    public class AppointmentCreatedEvent : IntegrationEvent
    {
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Guid DoctorId { get; set; }
    }
}
