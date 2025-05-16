using System.ComponentModel.DataAnnotations;

namespace ExamSystem.App.Areas.Admin.Models
{
    public class QuestionViewModel
    {
        public int? QuestionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CorrectChoice { get; set; }
        [Required(ErrorMessage="* This field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage="This field has to be 3 characters at least.")]
        public string QuestionTitle { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50)]
        public string FirstChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50)]
        public string SecondChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50)]
        public string ThirdChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50)]
        public string FourthChoice { get; set; } = null!;
        public int ExamId { get; set; }
    }
}
