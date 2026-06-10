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

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            // Kontrola role
            if (user.IsTeacher)
            {
                return View("TeacherDashboard", user);
            }
            else
            {
                return View("StudentDashboard", user);
            }
        }

        [Authorize]
        public async Task<IActionResult> TeacherDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
                return Forbid();

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> StudentDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.IsTeacher)
                return Forbid();

            return View(user);
        }
    }
}
