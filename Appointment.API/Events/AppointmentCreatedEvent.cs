using MessageBroker.Base.Events;

namespace Appointment.API.Events
{
    public class AppointmentCreatedEvent : IntegrationEvent
    {
        public AppointmentCreatedEvent(Guid appointmentId, DateTime appointmentDate, Guid doctorId)
        {
            AppointmentId = appointmentId;
            AppointmentDate = appointmentDate;
            DoctorId = doctorId;
        }

        private AppointmentCreatedEvent()
        {
        }
         

        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Guid DoctorId { get; set; }
    }
}
