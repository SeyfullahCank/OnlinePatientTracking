namespace Patient.API.Models
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public void SetId()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
