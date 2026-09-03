namespace Skolni_portal.Data
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? TeacherId { get; set; }
        public ApplicationUser? Teacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<StudentClass> Students { get; set; } = new List<StudentClass>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
}
