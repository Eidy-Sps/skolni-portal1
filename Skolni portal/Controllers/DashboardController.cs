using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skolni_portal.Data;

namespace Skolni_portal.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            ILogger<DashboardController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Teacher"))
            {
                return RedirectToAction("TeacherDashboard");
            }
            else if (roles.Contains("Student"))
            {
                return RedirectToAction("StudentDashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> TeacherDashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            // Zde budou data pro učitele
            ViewData["UserEmail"] = user?.Email;
            ViewData["UserName"] = user?.UserName;

            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentDashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            // Zde budou data pro žáky
            ViewData["UserEmail"] = user?.Email;
            ViewData["UserName"] = user?.UserName;

            return View();
        }
    }
}
