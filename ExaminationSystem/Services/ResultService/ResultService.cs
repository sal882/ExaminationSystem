using ExaminationSystem.Models;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.Exam;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Services.ResultService
{
    public class ResultService:IResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ExamResultViewModel> GetStudentResult(int studentId, int examId)
        {
            var resultRepo = _unitOfWork.Repository<StudentExamResult>();
            var result = resultRepo.GetAll()
                .FirstOrDefault(r => r.StudentID == studentId && r.ExamID == examId);

            if (result == null) return null;

            return new ExamResultViewModel
            {
                StudentID = result.StudentID,
                Score = result.Score,
                SubmittedAt = result.SubmittedAt
            };
        }
        public async Task<IEnumerable<ExamResultViewModel>> GetAllResultsForExam(int examId)
        {
            var resultRepo = _unitOfWork.Repository<StudentExamResult>();
            var results = await resultRepo.GetAll()
                .Where(r => r.ExamID == examId)
                .ToListAsync();

            return results.Select(r => new ExamResultViewModel
            {
                StudentID = r.StudentID,
                Score = r.Score,
                SubmittedAt = r.SubmittedAt
            }).ToList();
        }
    }
}
