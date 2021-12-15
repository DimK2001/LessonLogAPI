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
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Attendances)
                .WithOne(e => e.Student)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Students)
                .WithOne(e => e.Group)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.GroupId);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Attendances)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Teachers)
                .WithOne(e => e.Lesson)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lesson>().OwnsOne(u => u.Classroom);
            modelBuilder.Entity<Lesson>().HasOne(u => u.Subject);
        }
    }
}
