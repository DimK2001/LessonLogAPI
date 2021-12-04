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
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasOne(e => e.Student)
                .WithMany(e => e.Attendances)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Students)
                .WithOne(e => e.Group)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.GroupId)
                .IsRequired(true);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.LessonId)
                .IsRequired(true);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Attendances)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Teachers)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lesson>()
                .HasOne(e => e.Classroom)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Lessons)
                .WithOne(e => e.Subject)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);
        }
    }
}
