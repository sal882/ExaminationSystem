namespace ExaminationSystem.Models
{
    public class Course:BaseEntity
    {
        public int InstructorID { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public Instructor Instructor { get; set; }
        public HashSet<Exam>? Exams { get; set; }
        public HashSet<StudentCourse> StudentCourses { get; set; }
    }
}
