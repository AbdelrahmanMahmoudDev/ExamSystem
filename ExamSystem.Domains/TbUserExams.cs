using System;

namespace ExamSystem.Domains
{
    public class TbUserExams
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool PassStatus { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Score { get; set; }
        public string? UserId { get; set; } = null!;
        public int ExamId { get; set; }
        public TbExams Exam { get; set; } = null!;
    }
}
