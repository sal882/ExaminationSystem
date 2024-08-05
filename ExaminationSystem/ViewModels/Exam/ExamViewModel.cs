using ExaminationSystem.Models;

namespace ExaminationSystem.ViewModels.Exam
{
    public class ExamViewModel
    {
        public DateTime StartDate { get; set; }
        public int TotalGrade { get; set; }
        public ExamType Type { get; set; } // Quiz-Final
        public int QuestionsNumber { get; set; }
        public List<int> QuestionsIDs { get; set; }
    }
}
