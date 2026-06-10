namespace Skolni_portal.Data
{
    public class Grade
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public int GradeValue { get; set; } // 1-5
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ApplicationUser? Student { get; set; }
        public virtual ApplicationUser? Teacher { get; set; }
    }
}
