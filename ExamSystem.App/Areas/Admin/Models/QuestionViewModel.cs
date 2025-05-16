using System.ComponentModel.DataAnnotations;

namespace ExamSystem.App.Areas.Admin.Models
{
    public class QuestionViewModel
    {
        public int? QuestionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CorrectChoice { get; set; }
        [Required(ErrorMessage="* This field is required.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage="This field has to be 10 characters at least.")]
        public string QuestionTitle { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, ErrorMessage = "This field has to be 5 characters at least.")]
        public string FirstChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, ErrorMessage = "This field has to be 5 characters at least.")]
        public string SecondChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, ErrorMessage = "This field has to be 5 characters at least.")]
        public string ThirdChoice { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, ErrorMessage = "This field has to be 5 characters at least.")]
        public string FourthChoice { get; set; } = null!;
        public int ExamId { get; set; }
    }
}
