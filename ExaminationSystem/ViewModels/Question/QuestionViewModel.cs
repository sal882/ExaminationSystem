using ExaminationSystem.DTOs.QuestionDto;
using ExaminationSystem.Models;

namespace ExaminationSystem.ViewModels.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Level { get; set; }
        public List<ChoiceDto> Choices { get; set; }
    }
}
