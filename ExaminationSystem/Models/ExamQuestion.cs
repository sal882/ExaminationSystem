namespace ExaminationSystem.Models
{
    public class ExamQuestion:BaseEntity
    {
        public int ExamID { get; set; }
<<<<<<< HEAD
        public int Grade { get; set; }
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public Exam Exam { get; set; }
    }
}