namespace ExaminationSystem.Models
{
    public class Exam:BaseEntity
    {
 
        
        public DateTime StartDate { get; set; }
        public int TotalGrade { get; set; }

        public ExamType Type { get; set; } // Quiz-Final
        public int QuestionsNumber { get; set; }
        public int InstructorID { get; set; }
        public Instructor Instructor { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public HashSet<ExamQuestion> ExamQuestions { get; set; }
        public HashSet<StudentExam> StudentExams { get; set; }
    }
}
