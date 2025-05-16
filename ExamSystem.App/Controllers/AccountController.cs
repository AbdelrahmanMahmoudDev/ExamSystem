using ExamSystem.App.Models;
using ExamSystem.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Roles = _roleManager.Roles
            };
            return View(registerViewModel);
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", userViewModel);
            }

            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.UserName = userViewModel.UserName;
            applicationUser.Email = userViewModel.Email;

            IdentityResult userCreationResult = await _userManager.CreateAsync(applicationUser, userViewModel.Password);

            if (userCreationResult.Succeeded)
            {
                IdentityResult OpResult = await _userManager.AddToRoleAsync(applicationUser, userViewModel.RoleName);

                if (!OpResult.Succeeded)
                {
                    foreach (var error in OpResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                await _signInManager.SignInAsync(applicationUser, userViewModel.IsRememberMe);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in userCreationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", userViewModel);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, userViewModel.IsRememberMe, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username and password.");

            return View("Login", userViewModel);
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
