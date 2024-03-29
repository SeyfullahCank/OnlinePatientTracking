namespace Notification.API.Models
{
    public class Patient
    {
        private Patient()
        {
            
        }
        public Patient(Guid id, string fullName)
        {
            this.Id = id;
            this.FullName = fullName;
        }
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
    }
}
