using Microsoft.AspNetCore.Identity;
using ExamSystem.Domains;
namespace ExamSystem.DAL.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserExams = new List<TbUserExams>();
            Exams = new List<TbExams>();
        }

        public ICollection<TbUserExams> UserExams { get; set; }
        public ICollection<TbExams> Exams { get; set; }
    }
}
