namespace ExamSystem.Domains
{
    public class TbExams
    {
        public TbExams()
        {
            Questions = new List<TbQuestions>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public ICollection<TbQuestions> Questions { get; set; }
    }
}
