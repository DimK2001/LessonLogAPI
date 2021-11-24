using Microsoft.EntityFrameworkCore;
using LessonLog.Domain;

namespace LessonLog.Infrastructure
{
    public class LessonLogContext: DbContext
    {
        public LessonLogContext(DbContextOptions<LessonLogContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance> Attendences { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Сlassroom> Сlassrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasOne(e => e.Student)
                .WithMany(e => e.Attendances)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.StudentId)
                .IsRequired(true);
            modelBuilder.Entity<Student>()
                .HasOne(e => e.Group)
                .WithMany(e => e.Students)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.GroupId)
                .IsRequired(true);
            modelBuilder.Entity<Group>()
                .HasOne(e => e.Lesson)
                .WithMany(e => e.Groups)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.LessonId)
                .IsRequired(false);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Attendances)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
