using ExamSystem.DAL.Identity;
using ExamSystem.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.DAL.Context
{
    public class MainContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<TbExams> TbExams { get; set; }
        public virtual DbSet<TbUserExams> TbUserExams { get; set; }
        public virtual DbSet<TbQuestions> TbQuestions { get; set; }

        public MainContext() : base() { }
        public MainContext(DbContextOptions options) : base(options) { Database.EnsureCreated(); }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(300);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TbExams>(e =>
            {
                e.HasKey(e => e.Id);
            });

            builder.Entity<TbQuestions>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(e => e.Exam)
                 .WithMany(e => e.Questions)
                 .HasForeignKey(e => e.ExamId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TbUserExams>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(e => e.Exam)
                 .WithMany(e => e.UserExams)
                 .HasForeignKey(e => e.ExamId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ApplicationUser>(e =>
            {
                e.HasMany(e => e.UserExams)
                 .WithOne()
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(e => e.Exams)
                 .WithOne()
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}