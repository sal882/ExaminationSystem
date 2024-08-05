using ExaminationSystem.DTOs.Exam;
using ExaminationSystem.Models;
using ExaminationSystem.ViewModels.Exam;

namespace ExaminationSystem.Services.ExamService
{
    public interface IExamService
    {
        Task<ExamViewModel> CreateManualExam(ManualExamDto manualExamDto);
        Task<ExamViewModel> CreateAutoExam(AutoExamDto autoExamDto);
        Task<ExamViewModel> GetExamById(int id);
        Task<bool> UpdateAutoExam(int Id,AutoExamDto autoExamDto);
        Task<bool> UpdateManualExam(int Id,ManualExamDto manualExamDto);
        Task<ExamViewModel> GetExamForCourse(int courseId);
        Task<bool> SubmitExam(SubmitExamDto submitExamDto);
        Task<Exam> StartExam(int studentId, int examId);
        Task<bool> AssignStudentToExams(int examId, int studentId);
        Task<bool> DeleteExamAsync(int id);
    }
}
