using ExaminationSystem.ViewModels.Exam;
using System.Threading.Tasks;

namespace ExaminationSystem.Services.ResultService
{
    public interface IResultService
    {
        Task<ExamResultViewModel> GetStudentResult(int studentId, int examId);
        Task<IEnumerable<ExamResultViewModel>> GetAllResultsForExam(int examId);
    }
}
