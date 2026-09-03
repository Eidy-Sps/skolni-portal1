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
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Absence> Absences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Inicializace s výchozím správním kódem
            modelBuilder.Entity<TeacherCode>().HasData(
                new TeacherCode { Id = 1, Code = "UCITEL2026", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) }
            );

            // Konfiguracja Grade relationships
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany()
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Teacher)
                .WithMany()
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            // Konfiguracja Schedule relationships
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracia StudentClass (Many-to-Many)
            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Student)
                .WithMany()
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracia Absence relationships
            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Class)
                .WithMany(c => c.Absences)
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.RecordedByUser)
                .WithMany()
                .HasForeignKey(a => a.RecordedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Konfiguracia SchoolClass relationships
            modelBuilder.Entity<SchoolClass>()
                .HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}