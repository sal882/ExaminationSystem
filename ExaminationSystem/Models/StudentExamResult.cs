namespace ExaminationSystem.Models
{
    public class StudentExamResult : BaseEntity
    {
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public double Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
