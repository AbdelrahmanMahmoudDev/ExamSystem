using System.ComponentModel.DataAnnotations;

namespace ExamSystem.App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "* This field is required.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "* This field is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be atleast 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool IsRememberMe { get; set; }
    }
}
