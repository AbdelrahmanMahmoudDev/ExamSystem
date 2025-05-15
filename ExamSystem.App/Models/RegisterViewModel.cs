using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExamSystem.App.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "* This field is required.")]
        public string UserName { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [Required(ErrorMessage = "* This field is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be atleast 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool IsRememberMe { get; set; }
        [Required(ErrorMessage = "* This field is required.")]
        public string RoleName { get; set; } = null!;
        [ValidateNever]
        public IQueryable<IdentityRole> Roles { get; set; } = null!;
    }
}
