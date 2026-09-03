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
        public string Subject { get; set; } = string.Empty; // např. "Matematika"
        public bool IsExcused { get; set; } = false; // omluvená/neomluvená
        public string? Reason { get; set; } // důvod absence

        public string? RecordedByUserId { get; set; } // který učitel to zadal
        public ApplicationUser? RecordedByUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
