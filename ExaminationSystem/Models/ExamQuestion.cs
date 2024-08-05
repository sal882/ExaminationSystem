namespace ExaminationSystem.Models
{
    public class ExamQuestion:BaseEntity
    {
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public Exam Exam { get; set; }
    }
}