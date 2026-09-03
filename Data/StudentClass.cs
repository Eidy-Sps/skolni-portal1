namespace Skolni_portal.Data
{
    public class StudentClass
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public ApplicationUser? Student { get; set; }

        public int ClassId { get; set; }
        public SchoolClass? Class { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.Now;
    }
}
