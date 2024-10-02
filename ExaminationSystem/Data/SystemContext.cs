using ExaminationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExaminationSystem.Data
{
    public class SystemContext:DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) 
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<User> Users { get; set; }
        DbSet<ExamQuestion> ExamQuestions { get; set; }
        DbSet<StudentCourse> StudentCourses { get; set; }
        DbSet<StudentExam> StudentExams { get; set; }

        DbSet<StudentExamResult> StudentExamResults { get; set; }


    }
}
