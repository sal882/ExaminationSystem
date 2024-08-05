using ExaminationSystem.Models;

namespace ExaminationSystem.DTOs.Exam
{
    public class ManualExamDto
    {
        public DateTime StartDate { get; set; }
        public int TotalGrade { get; set; }
        public ExamType Type { get; set; } // Quiz-Final
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public List<int> QuestionsIDs { get; set; }
    }
}
