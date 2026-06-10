using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;

namespace Skolni_portal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AdminController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> TeacherCodes()
        {
            // Ověření, že je uživatel administrátor (v budoucnu můžete přidat roli)
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.Email.Contains("admin"))
            {
                TempData["ErrorMessage"] = "Nemáte oprávnění přistupovat k administrativnímu panelu.";
                return RedirectToAction("Index", "Home");
            }

            var codes = await _context.TeacherCodes.ToListAsync();
            return View(codes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacherCode(string code)
        {
            // Ověření administrátora
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.Email.Contains("admin"))
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                TempData["ErrorMessage"] = "Kód nemůže být prázdný.";
                return RedirectToAction("TeacherCodes");
            }

            var existingCode = await _context.TeacherCodes
                .FirstOrDefaultAsync(tc => tc.Code == code);

            if (existingCode != null)
            {
                TempData["ErrorMessage"] = "Tento kód již existuje.";
                return RedirectToAction("TeacherCodes");
            }

            var teacherCode = new TeacherCode
            {
                Code = code.ToUpper(),
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.TeacherCodes.Add(teacherCode);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Nový kód učitele vytvořen: {Code}", code);
            TempData["SuccessMessage"] = "Kód byl úspěšně vytvořen.";

            return RedirectToAction("TeacherCodes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateTeacherCode(int id)
        {
            // Ověření administrátora
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.Email.Contains("admin"))
            {
                return Unauthorized();
            }

            var code = await _context.TeacherCodes.FindAsync(id);
            if (code == null)
            {
                TempData["ErrorMessage"] = "Kód nebyl nalezen.";
                return RedirectToAction("TeacherCodes");
            }

            code.IsActive = false;
            _context.TeacherCodes.Update(code);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Kód učitele deaktivován: {Code}", code.Code);
            TempData["SuccessMessage"] = "Kód byl deaktivován.";

            return RedirectToAction("TeacherCodes");
        }

        [HttpGet]
        public async Task<IActionResult> Teachers()
        {
            // Ověření administrátora
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.Email.Contains("admin"))
            {
                TempData["ErrorMessage"] = "Nemáte oprávnění přistupovat k administrativnímu panelu.";
                return RedirectToAction("Index", "Home");
            }

            var teachers = await _context.Users
                .Where(u => u.IsTeacher)
                .ToListAsync();

            return View(teachers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTeacherRole(string id)
        {
            // Ověření administrátora
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.Email.Contains("admin"))
            {
                return Unauthorized();
            }

            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null)
            {
                TempData["ErrorMessage"] = "Učitel nebyl nalezen.";
                return RedirectToAction("Teachers");
            }

            teacher.IsTeacher = false;
            var result = await _userManager.UpdateAsync(teacher);

            if (result.Succeeded)
            {
                _logger.LogInformation("Roli učitele odebírán: {Email}", teacher.Email);
                TempData["SuccessMessage"] = "Role učitele byla odstraněna.";
            }
            else
            {
                TempData["ErrorMessage"] = "Při odstraňování role došlo k chybě.";
            }

            return RedirectToAction("Teachers");
        }
    }
}
