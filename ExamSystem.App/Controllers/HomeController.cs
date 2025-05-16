using ExamSystem.App.Areas.Admin.Models;
using ExamSystem.BL;
using ExamSystem.DAL.Identity;
using ExamSystem.Domains;
using ExamSystem.Domains.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.App.Controllers
{
}
[Authorize]
public class HomeController : Controller
{
    private readonly IExamService _examService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserExamService _userExamService;
    public HomeController(IExamService examService, UserManager<ApplicationUser> userManager, IUserExamService userExamService)
    {
        _examService = examService;
        _userManager = userManager;
        _userExamService = userExamService;
    }

    public async Task<IActionResult> Index()
    {
        var allExams = await _examService.GetAllExams();
        return View(allExams.ToList());
    }

    public IActionResult SolveExam(int examId)
    {
        if (examId <= 0) return BadRequest("Invalid exam ID");

        var exam = _examService.GetExamBasedOnId(examId).Result;

        if (exam == null) return NotFound("Exam not found");

        {
            var examViewModel = new ExamViewModel
            {
                Id = exam.Id,
                Title = exam.Title,
                Questions = exam.Questions.Select(q => new QuestionViewModel
                {
                    QuestionId = q.Id,
                    QuestionTitle = q.Title,
                    FirstChoice = q.FirstChoice,
                    SecondChoice = q.SecondChoice,
                    ThirdChoice = q.ThirdChoice,
                    FourthChoice = q.FourthChoice,
                    CorrectChoice = q.CorrectChoice
                }).ToList()
            };

            return View(examViewModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/Home/SubmitExamAjax")]
    public async Task<IActionResult> SubmitExamAjax([FromBody] ExamAttemptDTO examAttemptDTO)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        ApplicationUser currentUser = await _userManager.GetUserAsync(User);

        var examResult = await _userExamService.AddAttemptToDb(currentUser.Id, examAttemptDTO);

        if (examResult == null)
        {
            return Json(new { success = false, message = "Failed to save exam result" });
        }

        var payload = new
        {
            score = examResult.Item1,
            correctAnswers = examResult.Item2,
            inCorrectAnswers = examResult.Item3,
            passStatus = examResult.Item4
        };

        return Json(new { success = true, message = "exam submission is successful", payload });
    }
}
