using ExaminationSystem.Models;

namespace ExaminationSystem.DTOs.Exam
{
    public class AutoExamDto
    {
        public DateTime StartDate { get; set; }
        public int TotalGrade { get; set; }
        public ExamType Type { get; set; } // Quiz-Final
        public int QuestionsNumber { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
    }
}
