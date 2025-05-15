using ExamSystem.App.Areas.Admin.Models;
using ExamSystem.BL;
using ExamSystem.DAL.Identity;
using ExamSystem.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExamController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IExamService _examService;

        public ExamController(UserManager<ApplicationUser> userManager, IExamService examService)
        {
            _userManager = userManager;
            _examService = examService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAjax([FromBody] ExamViewModel examViewModel)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, message = "Invalid data provided.", errors = errors });
            }

            TbExams newExam = new TbExams()
            {
                Title = examViewModel.Title
            };

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            newExam.UserId = currentUser.Id;

            bool saveResult = await _examService.SaveNew(newExam);

            if(!saveResult)
            {
                return BadRequest(new { success = false, message = "Failed to save the exam.", errors = new[] { "An error occurred while saving the exam. Please report this to customer support" } });
            }

            return Ok(new { success = true, message = "Exam updated successfully." });
        }
    }
}
