using MessageBroker.Base.Events;

namespace Patient.API.Events
{
    public class PatientCreatedEvent : IntegrationEvent
    {
        private PatientCreatedEvent()
        {
            
        }
        public PatientCreatedEvent(Guid patientId, string fullName)
        {
            PatientId = patientId;
            FullName = fullName;
        }

        public Guid PatientId { get; private set; }
        public string FullName { get; private set; }
    }
}
