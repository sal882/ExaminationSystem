namespace ExaminationSystem.ViewModels.Question
{
    public class ChoicesViewModel
    {
        public class ChoiceViewModel
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}
