using ExaminationSystem.DTOs.QuestionDto;
using ExaminationSystem.ViewModels.Question;

namespace ExaminationSystem.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<QuestionViewModel> CreateQuestionAsync(QuestionDto createQuestionDto);
        Task<QuestionViewModel> UpdateQuestionAsync(QuestionDto updateQuestionDto,int id);
        Task<bool> DeleteQuestionAsync(int id);
        Task AssignGradeToQuestionAsync(int examId, int questionId, int grade);
        Task<QuestionViewModel> GetQuestionByIdAsync(int id);
    }
}
