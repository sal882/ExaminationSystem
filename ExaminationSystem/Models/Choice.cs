namespace ExaminationSystem.Models
{
    public class Choice:BaseEntity
    {
        public string Text { get; set; }
        public Question Question { get; set; }
        public int QuestionID { get; set; }
        public bool IsRight { get; set; }
    }
}