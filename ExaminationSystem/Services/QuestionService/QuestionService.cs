using AutoMapper;
using ExaminationSystem.DTOs.QuestionDto;
using ExaminationSystem.Models;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.Question;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Services.QuestionService
{
    public class QuestionService:IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QuestionViewModel> CreateQuestionAsync(QuestionDto createQuestionDto)
        {

            if (createQuestionDto == null)
            {
                throw new ArgumentNullException(nameof(createQuestionDto), "CreateQuestionDto cannot be null.");
            }

            var question = _mapper.Map<Question>(createQuestionDto);
            _unitOfWork.Repository<Question>().Add(question);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<QuestionViewModel>(question);
        }

        public async Task<QuestionViewModel> UpdateQuestionAsync(QuestionDto updateQuestionDto, int id)
        {
            if (updateQuestionDto == null)
            {
                throw new ArgumentNullException(nameof(updateQuestionDto), "UpdateQuestionDto cannot be null.");
            }

            var question =_unitOfWork.Repository<Question>().GetByID(id);
            if (question == null)
            {
                throw new KeyNotFoundException("The specified question does not exist.");
            }

            _mapper.Map(updateQuestionDto, question);
            _unitOfWork.Repository<Question>().Update(question);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<QuestionViewModel>(question);
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = _unitOfWork.Repository<Question>().GetByID(id);
            if (question == null)
            {
                throw new KeyNotFoundException("The specified question does not exist.");
            }

            _unitOfWork.Repository<Question>().Delete(question);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task AssignGradeToQuestionAsync(int examId, int questionId, int grade)
        {
            var examQuestion = await _unitOfWork.Repository<ExamQuestion>()
              .GetAll().FirstOrDefaultAsync(eq => eq.ExamID == examId && eq.QuestionID == questionId);

            if (examQuestion == null)
            {
                throw new KeyNotFoundException("The specified ExamQuestion does not exist.");
            }

            examQuestion.Grade = grade;
            _unitOfWork.Repository<ExamQuestion>().Update(examQuestion);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<QuestionViewModel> GetQuestionByIdAsync(int id)
        {
            var question = await _unitOfWork.Repository<Question>() 
                                    .GetAll() // Assuming GetAll() includes navigation properties
                                    .Include(q => q.choices)
                                    .FirstOrDefaultAsync(q => q.Id == id); ;
            if (question == null)
            {
                throw new KeyNotFoundException("The specified question does not exist.");
            }

            return _mapper.Map<QuestionViewModel>(question);
        }
    }
}
