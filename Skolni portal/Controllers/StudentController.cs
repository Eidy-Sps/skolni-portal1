using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;

namespace Skolni_portal.Controllers
{
    [Authorize]
    [Route("Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<StudentController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: /Student/Schedule
        [HttpGet("Schedule")]
        public async Task<IActionResult> Schedule()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            // Kontrola, aby učitelé neviděli rozvrh
            if (user.IsTeacher)
            {
                return Forbid();
            }

            // Načtení rozvrhu pro aktuálního žáka
            var schedules = await _context.Schedules
                .Where(s => s.StudentId == user.Id)
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();

            // Pokud žák nemá rozvrh, vytvoříme demo rozvrh
            if (!schedules.Any())
            {
                schedules = CreateDemoSchedule(user.Id);
                await _context.SaveChangesAsync();
            }

            ViewBag.DayNames = new[] { "Pondělí", "Úterý", "Středa", "Čtvrtek", "Pátek" };

            return View(schedules);
        }

        // GET: /Student/Grades
        [HttpGet("Grades")]
        public async Task<IActionResult> Grades()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            // Kontrola, aby učitelé neviděli své známky
            if (user.IsTeacher)
            {
                return Forbid();
            }

            // Načtení známek pro aktuálního žáka
            var grades = await _context.Grades
                .Where(g => g.StudentId == user.Id)
                .Include(g => g.Teacher)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();

            return View(grades);
        }

        // Pomocná metoda pro vytvoření demo rozvrhu
        private List<Schedule> CreateDemoSchedule(string studentId)
        {
            var schedules = new List<Schedule>
            {
                // Pondělí
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 0,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(8, 45, 0),
                    SubjectName = "Český jazyk",
                    TeacherName = "Mgr. Jana Nováková",
                    Classroom = "102"
                },
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 0,
                    StartTime = new TimeSpan(8, 55, 0),
                    EndTime = new TimeSpan(9, 40, 0),
                    SubjectName = "Matematika",
                    TeacherName = "Mgr. Petr Dvořák",
                    Classroom = "201"
                },
                // Úterý
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 1,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(8, 45, 0),
                    SubjectName = "Anglický jazyk",
                    TeacherName = "Mgr. Michaela Svobodová",
                    Classroom = "105"
                },
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 1,
                    StartTime = new TimeSpan(8, 55, 0),
                    EndTime = new TimeSpan(9, 40, 0),
                    SubjectName = "Informatika",
                    TeacherName = "Mgr. Tomáš Kučera",
                    Classroom = "304"
                },
                // Středa
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 2,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(8, 45, 0),
                    SubjectName = "Fyzika",
                    TeacherName = "Mgr. Zdeněk Navrátil",
                    Classroom = "203"
                },
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 2,
                    StartTime = new TimeSpan(8, 55, 0),
                    EndTime = new TimeSpan(9, 40, 0),
                    SubjectName = "Chemie",
                    TeacherName = "Mgr. Helena Králová",
                    Classroom = "205"
                },
                // Čtvrtek
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 3,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(8, 45, 0),
                    SubjectName = "Dějepis",
                    TeacherName = "Mgr. Václav Musil",
                    Classroom = "103"
                },
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 3,
                    StartTime = new TimeSpan(8, 55, 0),
                    EndTime = new TimeSpan(9, 40, 0),
                    SubjectName = "Zemepis",
                    TeacherName = "Mgr. Petra Šmídová",
                    Classroom = "104"
                },
                // Pátek
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 4,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(8, 45, 0),
                    SubjectName = "Tělesná výchova",
                    TeacherName = "Mgr. Karel Horák",
                    Classroom = "Tělocvična"
                },
                new Schedule
                {
                    StudentId = studentId,
                    ClassName = "1.A",
                    DayOfWeek = 4,
                    StartTime = new TimeSpan(8, 55, 0),
                    EndTime = new TimeSpan(9, 40, 0),
                    SubjectName = "Hudobná výchova",
                    TeacherName = "Mgr. Vladimír Sova",
                    Classroom = "201"
                }
            };

            foreach (var schedule in schedules)
            {
                _context.Schedules.Add(schedule);
            }

            return schedules;
        }
    }
}
