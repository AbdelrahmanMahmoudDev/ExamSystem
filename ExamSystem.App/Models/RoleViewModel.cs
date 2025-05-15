using System.ComponentModel.DataAnnotations;

namespace ExamSystem.App.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "* This field is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A role name must be atleast 3 characters long.")]
        public string RoleName { get; set; } = string.Empty;
    }
}
