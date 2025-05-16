namespace ExamSystem.App.Areas.Admin.Models
{
    public class ExamListViewModel
    {
        public ExamViewModel ExamViewModel { get; set; } = null!;
        public IEnumerable<ExamViewModel> ExamViewModelList { get; set; } = null!;
        public List<QuestionViewModel> QuestionViewModelList { get; set; } = null!;
        public QuestionViewModel QuestionViewModel{ get; set; } = null!;
    }
}
