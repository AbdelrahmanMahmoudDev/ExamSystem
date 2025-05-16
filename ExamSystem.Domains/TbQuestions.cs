namespace ExamSystem.Domains
{
    public class TbQuestions
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CorrectChoice { get; set; }
        public int UserChoice { get; set; }
        public string Title { get; set; } = null!;
        public string FirstChoice { get; set; } = null!;
        public string SecondChoice { get; set; } = null!;
        public string ThirdChoice { get; set; } = null!;
        public string FourthChoice { get; set; } = null!;
        public int ExamId { get; set; }
        public TbExams Exam { get; set; } = null!;
    }
}
