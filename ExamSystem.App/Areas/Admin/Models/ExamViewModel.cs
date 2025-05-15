using System.ComponentModel.DataAnnotations;
using ExamSystem.Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExamSystem.App.Areas.Admin.Models
{
    public class ExamViewModel
    {
        [ValidateNever]
        public int? Id { get; set; }
        [Required(ErrorMessage = "The book title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The book title must be atleast 3 characters long.")]
        public string Title { get; set; } = null!;
    }
}
