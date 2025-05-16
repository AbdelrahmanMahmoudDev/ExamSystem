using System.ComponentModel.DataAnnotations;
using ExamSystem.Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExamSystem.App.Areas.Admin.Models
{
    public class ExamViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The exam title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The exam title must be atleast 3 characters long.")]
        public string Title { get; set; } = null!;

        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }
}