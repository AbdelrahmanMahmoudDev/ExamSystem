using System;

namespace ExamSystem.Domains
{
    public class TbUserExams
    {
        public TbUserExams()
        {
            UserAnswers = new List<TbUserAnswers>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool PassStatus { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Score { get; set; }
        public string? UserId { get; set; } = null!;
        public ICollection<TbUserAnswers> UserAnswers { get; set; }
    }
}
