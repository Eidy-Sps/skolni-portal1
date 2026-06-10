using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Skolni_portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TeacherCode> TeacherCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Inicializace s výchozím správním kódem
            modelBuilder.Entity<TeacherCode>().HasData(
                new TeacherCode { Id = 1, Code = "UCITEL2026", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) }
            );
        }
    }
}