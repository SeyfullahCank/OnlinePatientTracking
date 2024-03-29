namespace AnswerQuestion.API.Models
{
    public class Answer
    {
        public Guid Id { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; }
        public string AnswerOfDoctor { get; private set; }
    }
}
