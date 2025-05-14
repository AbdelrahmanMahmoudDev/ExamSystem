namespace ExamSystem.Domains
{
    public class TbUserAnswers
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public int SelectedAnswer { get; set; }
        public int QuestionId { get; set; }
        public TbQuestions Question { get; set; } = null!;
        public int UserExamId { get; set; }
        public TbUserExams UserExam { get; set; } = null!;
    }
}
