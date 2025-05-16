namespace ExamSystem.Domains.DTOs
{
    public class ExamAttemptQuestion
    {
        public int QuestionId { get; set; }
        public int CorrectChoice { get; set; }
        public int UserChoice { get; set; }
    }

    public class ExamAttemptDTO
    {
        public int ExamId { get; set; }
        public List<ExamAttemptQuestion> Questions { get; set; } = null!;
    }
}
