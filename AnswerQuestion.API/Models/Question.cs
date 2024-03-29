namespace AnswerQuestion.API.Models
{
    public class Question
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid DepartmentId { get; private set; }
        public string QuestionOfPatient { get; private set; }
        public DateTime CreatedDate { get; private set; }
    }
}
