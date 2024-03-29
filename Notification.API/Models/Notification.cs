namespace Notification.API.Models
{
    public class Notification
    {
        public Notification(Guid patientId, string message)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Message = message;
            NotificationDate = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(14);
            IsRead = false;
        }

        private Notification()
        {
            
        }
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public string Message { get; private set; }
        public DateTime NotificationDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsRead { get; private set; }
    }
}
