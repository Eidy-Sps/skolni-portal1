namespace Skolni_portal.Data
{
    public class Absence
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public ApplicationUser? Student { get; set; }

        public int ClassId { get; set; }
        public SchoolClass? Class { get; set; }

        public DateTime AbsenceDate { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool IsExcused { get; set; } = false;
        public string? Reason { get; set; }

        public string? RecordedByUserId { get; set; }
        public ApplicationUser? RecordedByUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
