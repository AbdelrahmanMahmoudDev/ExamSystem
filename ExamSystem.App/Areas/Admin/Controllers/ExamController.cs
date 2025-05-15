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

        public async Task<IActionResult> Index()
        {
            IEnumerable<TbExams> allExams = await _examService.GetAllExams();

            IEnumerable<ExamViewModel> allExamViewModels = allExams.Select(e => new ExamViewModel()
            {
                Id = e.Id,
                Title = e.Title
            });

            ExamListViewModel examListViewModel = new ExamListViewModel()
            {
                ExamViewModel = new ExamViewModel(),
                ExamViewModelList = allExamViewModels
            };

            return View(examListViewModel);
        }

        [HttpGet]
        [Route("GetAllExamsAjax/")]
        public async Task<IActionResult> GetAllExamsAjax()
        {
            var allExams = await _examService.GetAllExams();

            return Ok(allExams.Select(e => new
            {
                id = e.Id,
                title = e.Title,
            }));
        }

        [HttpGet]
        [Route("GetExamAjax/{id}")]
        public async Task<IActionResult> GetExamAjax(int id)
        {
            var exam = await _examService.GetExamBasedOnId(id);
            

            if (exam.Id == 0)
            {
                return NotFound(new { success = false, message = "Exam not found." });
            }

            return Ok(new
            {
                id = exam.Id,
                title = exam.Title,
            });
        }

        [HttpGet]
        [Route("DeleteExamAjax/{id}")]
        public async Task<IActionResult> DeleteExamAjax(int id)
        {
            var exam = await _examService.GetExamBasedOnId(id);

            if (exam.Id == 0)
            {
                return NotFound(new { success = false, message = "Exam not found." });
            }

            if(! await _examService.DeleteExam(exam))
            {
                return BadRequest(new { success = false, message = "Failed to delete the exam.", errors = new[] { "An error occurred while saving the exam. Please report this to customer support" } });
            }

            return Ok(new { success = true, message = "Exam deleted successfully." });
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

            if(examViewModel?.Id > 0)
            {
                var exam = await _examService.GetExamBasedOnId((int)examViewModel.Id);

                exam.Title = examViewModel.Title;

                if(!await _examService.SaveUpdate(exam))
                {
                    return BadRequest(new { success = false, message = "Failed to save the exam.", errors = new[] { "An error occurred while saving the exam. Please report this to customer support" } });
                }

                return Ok(new { success = true, message = "Exam updated successfully." });
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
