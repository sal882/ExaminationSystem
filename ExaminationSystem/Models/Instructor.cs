namespace ExaminationSystem.Models
{
    public class Instructor: User
    {
        public HashSet<Exam> Exams { get; set; }
        public HashSet<Course> Courses { get; set; }
    }
}
