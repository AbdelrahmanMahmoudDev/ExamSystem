using System.Data;
using ExamSystem.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.App.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", roleViewModel);
            }

            IdentityRole role = new IdentityRole()
            {
                Name = roleViewModel.RoleName,
            };


            IdentityResult OpResult = await _roleManager.CreateAsync(role);

            if(!OpResult.Succeeded)
            {
                foreach(var error in OpResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Index", "Account");
        }
    }
}
