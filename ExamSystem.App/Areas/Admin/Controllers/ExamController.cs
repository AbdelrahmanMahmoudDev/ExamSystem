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
        private readonly IQuestionService _questionService;

        public ExamController(UserManager<ApplicationUser> userManager, IExamService examService, IQuestionService questionService)
        {
            _userManager = userManager;
            _examService = examService;
            _questionService = questionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TbExams> allExams = await _examService.GetAllExams();
            return View(allExams.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            ExamViewModel examViewModel = new ExamViewModel();
            if (Id is not null)
            {
                TbExams targetExam = await _examService.GetExamBasedOnId((int)Id);

                examViewModel.Id = targetExam.Id;
                examViewModel.Title = targetExam.Title;
                examViewModel.Questions = targetExam.Questions.Select(s => new QuestionViewModel()
                {
                    CorrectChoice = s.CorrectChoice,
                    QuestionTitle = s.Title,
                    FirstChoice = s.FirstChoice,
                    SecondChoice = s.SecondChoice,
                    ThirdChoice = s.ThirdChoice,
                    FourthChoice = s.FourthChoice
                }).ToList();

                return View("Edit", examViewModel);
            }
            return View("Edit", examViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ExamViewModel examViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", examViewModel);
            }

            bool IsSuccess = false;
            TbExams newExam = new TbExams()
            {
                Id = examViewModel.Id,
                Title = examViewModel.Title,
                Questions = examViewModel.Questions.Select(s => new TbQuestions()
                {
                    CorrectChoice = s.CorrectChoice,
                    Title = s.QuestionTitle,
                    FirstChoice = s.FirstChoice,
                    SecondChoice = s.SecondChoice,
                    ThirdChoice = s.ThirdChoice,
                    FourthChoice = s.FourthChoice
                }).ToList()
            };

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            newExam.UserId = currentUser.Id;

            if (newExam.Id == 0)
                IsSuccess = await _examService.SaveNew(newExam);
            else
                IsSuccess = await _examService.SaveUpdate(newExam);

            if (!IsSuccess)
                return StatusCode(500, "A server side error occured while processing your request");

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Area("Admin")]
        [Route("Admin/Exam/DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            if (questionId <= 0) return BadRequest("Invalid question ID");

            try
            {
                await _questionService.RemoveQuestion(questionId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error deleting question");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            TbExams targetExam = await _examService.GetExamBasedOnId(id);
            if (!await _examService.DeleteExam(targetExam))
            {
                return StatusCode(500, "Error deleting question");
            }
            return RedirectToAction("Index");
        }
    }
}
