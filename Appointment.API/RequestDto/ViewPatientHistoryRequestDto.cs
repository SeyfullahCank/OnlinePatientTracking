namespace Appointment.API.RequestDto
{
    public class ViewPatientHistoryRequestDto
    {
        public DateTime? AppointmentDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
