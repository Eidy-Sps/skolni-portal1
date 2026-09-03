namespace Skolni_portal.Data
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // např. "3.A"
        public string Description { get; set; } = string.Empty;
        public int? TeacherId { get; set; }
        public ApplicationUser? Teacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Vztahy
        public ICollection<StudentClass> Students { get; set; } = new List<StudentClass>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
}
