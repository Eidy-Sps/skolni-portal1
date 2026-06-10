namespace Skolni_portal.Data
{
    public class Schedule
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public int DayOfWeek { get; set; } // 0 = Pondělí, 6 = Neděle
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string Classroom { get; set; } = string.Empty;

        // Navigation property
        public virtual ApplicationUser? Student { get; set; }
    }
}
