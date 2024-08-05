using ExaminationSystem.Models;

namespace ExaminationSystem.DTOs.QuestionDto
{
    public class QuestionDto
    {
        public string Text { get; set; }
        public QuestionLevel Level { get; set; }
        public Dictionary<string, bool> choices { get; set; } = new Dictionary<string, bool>();
    }
}
