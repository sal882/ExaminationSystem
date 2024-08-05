namespace ExaminationSystem.Models
{
    public class Result : BaseEntity
    {
        public int Score { get; set; }
        public int ExamId {  get; set; }
        public Exam Exam { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
