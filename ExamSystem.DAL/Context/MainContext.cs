using System;
using System.Reflection.Emit;
using ExamSystem.DAL.Identity;
using ExamSystem.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            builder.Entity<TbExams>().HasData(
                new TbExams { Id = 1, Title = "Math I", CreatedDate = DateTime.Now, UserId = null },
                new TbExams { Id = 2, Title = "Math II", CreatedDate = DateTime.Now, UserId = null }
            );

            builder.Entity<TbQuestions>().HasData(
                new TbQuestions
                {
                    Id = 1,
                    Title = "What is 2+2?",
                    CreatedDate = DateTime.Now,
                    CorrectChoice = 1,
                    UserChoice = 0,
                    FirstChoice = "4",
                    SecondChoice = "3",
                    ThirdChoice = "5",
                    FourthChoice = "2",
                    ExamId = 1
                },
                new TbQuestions
                {
                    Id = 2,
                    Title = "What is 5+5?",
                    CreatedDate = DateTime.Now,
                    CorrectChoice = 1,
                    UserChoice = 0,
                    FirstChoice = "10",
                    SecondChoice = "3",
                    ThirdChoice = "5",
                    FourthChoice = "2",
                    ExamId = 1
                },
                new TbQuestions
                {
                    Id = 3,
                    Title = "What is 5*5?",
                    CreatedDate = DateTime.Now,
                    CorrectChoice = 2,
                    UserChoice = 0,
                    FirstChoice = "10",
                    SecondChoice = "25",
                    ThirdChoice = "5",
                    FourthChoice = "2",
                    ExamId = 2
                },
                new TbQuestions
                {
                    Id = 4,
                    Title = "What is 7*5?",
                    CreatedDate = DateTime.Now,
                    CorrectChoice = 3,
                    UserChoice = 0,
                    FirstChoice = "10",
                    SecondChoice = "25",
                    ThirdChoice = "35",
                    FourthChoice = "2",
                    ExamId = 2
                }
            );
        }
    }
}