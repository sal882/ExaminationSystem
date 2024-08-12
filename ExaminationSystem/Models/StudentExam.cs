namespace ExaminationSystem.Models
{
    public class StudentExam:BaseEntity
    {
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
<<<<<<< HEAD
        public DateTime? StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public bool IsCompleted { get; set; }
        //public double Score { get; set; }
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}