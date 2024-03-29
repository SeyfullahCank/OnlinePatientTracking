namespace Appointment.API.Models
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid DepartmentId { get; set; }

        public void SetId()
        {
            this.Id = Guid.NewGuid();   
        }
    }
}
