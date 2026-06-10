using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;

namespace Skolni_portal.Controllers
{
    [Authorize]
    [Route("Teacher")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<TeacherController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: /Teacher/Grades
        [HttpGet("Grades")]
        public async Task<IActionResult> Grades()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
            {
                return Forbid();
            }

            // Načtení všech známek daného učitele
            var grades = await _context.Grades
                .Where(g => g.TeacherId == user.Id)
                .Include(g => g.Student)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();

            // Získání seznamu tříd a předmětů, které učitel vyučuje
            var classes = grades.Select(g => g.ClassName).Distinct().ToList();
            var subjects = grades.Select(g => g.SubjectName).Distinct().ToList();

            // Získání všech žáků (bez učitelů)
            var allStudents = await _userManager.Users
                .Where(u => !u.IsTeacher)
                .OrderBy(u => u.Email)
                .ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Subjects = subjects;
            ViewBag.Students = allStudents;

            return View(grades);
        }

        // POST: /Teacher/AddGrade
        [HttpPost("AddGrade")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade(string studentEmail, string subject, string className, int gradeValue)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
            {
                return Forbid();
            }

            // Validace hodnoty známky (1-5)
            if (gradeValue < 1 || gradeValue > 5)
            {
                return BadRequest("Známka musí být mezi 1 a 5.");
            }

            var student = await _userManager.FindByNameAsync(studentEmail);
            if (student == null)
            {
                return NotFound("Žák nebyl nalezen.");
            }

            var grade = new Grade
            {
                StudentId = student.Id,
                TeacherId = user.Id,
                SubjectName = subject,
                ClassName = className,
                GradeValue = gradeValue,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Učitel {user.Email} zadal známku {gradeValue} žákovi {student.Email} za {subject}");

            return RedirectToAction("Grades");
        }

        // POST: /Teacher/EditGrade
        [HttpPost("EditGrade/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrade(int id, int gradeValue)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
            {
                return Forbid();
            }

            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.Id == id && g.TeacherId == user.Id);
            if (grade == null)
            {
                return NotFound("Známka nebyla nalezena.");
            }

            if (gradeValue < 1 || gradeValue > 5)
            {
                return BadRequest("Známka musí být mezi 1 a 5.");
            }

            grade.GradeValue = gradeValue;
            grade.UpdatedAt = DateTime.Now;

            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Učitel {user.Email} upravil známku na {gradeValue}");

            return RedirectToAction("Grades");
        }

        // POST: /Teacher/DeleteGrade
        [HttpPost("DeleteGrade/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
            {
                return Forbid();
            }

            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.Id == id && g.TeacherId == user.Id);
            if (grade == null)
            {
                return NotFound("Známka nebyla nalezena.");
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Učitel {user.Email} smazal známku");

            return RedirectToAction("Grades");
        }
    }
}
