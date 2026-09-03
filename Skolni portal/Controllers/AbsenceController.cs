using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;

namespace Skolni_portal.Controllers
{
    [Authorize]
    public class AbsenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AbsenceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Přehled absencí pro třídu (Učitel)
        [Authorize]
        public async Task<IActionResult> ManageAbsences(int classId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
                return Forbid();

            var schoolClass = await _context.SchoolClasses
                .Include(c => c.Students)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == classId && c.TeacherId == user.Id);

            if (schoolClass == null)
                return NotFound();

            var absences = await _context.Absences
                .Where(a => a.ClassId == classId)
                .Include(a => a.Student)
                .OrderByDescending(a => a.AbsenceDate)
                .ToListAsync();

            ViewData["Class"] = schoolClass;
            return View(absences);
        }

        // Přidat novou absenci (Učitel)
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddAbsence(int classId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
                return Forbid();

            var schoolClass = await _context.SchoolClasses
                .Include(c => c.Students)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == classId && c.TeacherId == user.Id);

            if (schoolClass == null)
                return NotFound();

            ViewData["ClassId"] = classId;
            ViewData["ClassName"] = schoolClass.Name;
            ViewData["Students"] = schoolClass.Students;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAbsence(int classId, string studentId, DateTime absenceDate, 
            string subject, bool isExcused, string? reason)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
                return Forbid();

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(c => c.Id == classId && c.TeacherId == user.Id);

            if (schoolClass == null)
                return NotFound();

            var absence = new Absence
            {
                StudentId = studentId,
                ClassId = classId,
                AbsenceDate = absenceDate,
                Subject = subject,
                IsExcused = isExcused,
                Reason = reason,
                RecordedByUserId = user.Id,
                CreatedAt = DateTime.Now
            };

            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageAbsences", new { classId });
        }

        // Smazat absenci (Učitel)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAbsence(int absenceId, int classId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsTeacher)
                return Forbid();

            var absence = await _context.Absences
                .Include(a => a.Class)
                .FirstOrDefaultAsync(a => a.Id == absenceId);

            if (absence == null || absence.Class?.TeacherId != user.Id)
                return Forbid();

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageAbsences", new { classId });
        }

        // Přehled vlastních absencí (Žák)
        [Authorize]
        public async Task<IActionResult> MyAbsences()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.IsTeacher)
                return Forbid();

            var absences = await _context.Absences
                .Where(a => a.StudentId == user.Id)
                .Include(a => a.Class)
                .OrderByDescending(a => a.AbsenceDate)
                .ToListAsync();

            // Statistika
            var totalAbsences = absences.Count;
            var excusedAbsences = absences.Count(a => a.IsExcused);
            var unexcusedAbsences = totalAbsences - excusedAbsences;

            ViewData["TotalAbsences"] = totalAbsences;
            ViewData["ExcusedAbsences"] = excusedAbsences;
            ViewData["UnexcusedAbsences"] = unexcusedAbsences;

            return View(absences);
        }
    }
}
