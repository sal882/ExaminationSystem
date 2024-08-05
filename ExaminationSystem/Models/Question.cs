namespace ExaminationSystem.Models
{
    public class Question:BaseEntity
    {
        public string Text { get; set; }
        public QuestionLevel Level { get; set; } //Simple-Medium-Hard
        public HashSet<ExamQuestion> ExamQuestions { get; set; }
        public HashSet<Choice> choices { get; set; }
    }
}