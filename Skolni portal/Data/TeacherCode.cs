namespace Skolni_portal.Data
{
    public class TeacherCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
