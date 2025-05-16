using System.Diagnostics;
using ExamSystem.App.Models;
using ExamSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IExamService _examService;

        public HomeController(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<IActionResult> Index()
        {
            var allExams = await _examService.GetAllExams();
            return View(allExams.ToList());
        }
    }
}
