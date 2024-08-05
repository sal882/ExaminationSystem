namespace ExaminationSystem.Models
{
    public class Student:User
    {
        public HashSet<StudentCourse> StudentCourses { get; set; }
        public HashSet<StudentExam> StudentExams { get; set; }
    }
}
