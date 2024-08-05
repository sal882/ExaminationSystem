using AutoMapper;
using ExaminationSystem.DTOs.Exam;
using ExaminationSystem.Models;
using ExaminationSystem.Repositories;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.Exam;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Services.ExamService
{
    public class ExamService : IExamService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Question> _questionRepo;

        public ExamService(IMapper mapper,
            IGenericRepository<Exam>examRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<Question>questionRepo)
        {
            _mapper = mapper;
            _examRepository = examRepository;
            _unitOfWork = unitOfWork;
            _questionRepo = questionRepo;
        }
        public async Task<ExamViewModel> CreateAutoExam(AutoExamDto autoExamDto)
        {
            if (autoExamDto == null)
            {
                throw new ArgumentNullException(nameof(autoExamDto), "AutoExamDto cannot be null.");
            }
            var course = _unitOfWork.Repository<Course>().GetByID(autoExamDto.CourseID);
            if (course == null)
            {
                throw new KeyNotFoundException("The specified course does not exist.");
            }
            var instructor =_unitOfWork.Repository<Instructor>().GetByID(autoExamDto.InstructorID);
            if (instructor == null)
            {
                throw new KeyNotFoundException($"The instructor with ID {autoExamDto.InstructorID} does not exist.");
            }
            var exam = _mapper.Map<Exam>(autoExamDto);
            if (exam == null)
            {
                throw new InvalidOperationException("Failed to map AutoExamDto to Exam.");
            }
            _examRepository.Add(exam);
            await _unitOfWork.CompleteAsync();
            if (exam.ExamQuestions == null)
            {
                exam.ExamQuestions = new HashSet<ExamQuestion>();
            }
            var questions = await _questionRepo.ListAllAsync();
            var selectedQuestions = SelectQuestionsForAutoExam(questions, autoExamDto.QuestionsNumber);

            foreach (var question in selectedQuestions)
            {
                var examQuestion = new ExamQuestion
                {
                    ExamID = exam.Id,
                    QuestionID = question.Id
                };
                 exam.ExamQuestions.Add(examQuestion);
            }

            await _unitOfWork.CompleteAsync();

            var examViewModel = _mapper.Map<ExamViewModel>(exam);
            if (examViewModel == null)
            {
                throw new InvalidOperationException("Failed to map Exam to ExamViewModel.");
            }
            examViewModel.QuestionsIDs = selectedQuestions.Select(q => q.Id).ToList();
            return examViewModel;
        }

        public async Task<ExamViewModel> CreateManualExam(ManualExamDto manualExamDto)
        {
            var course = _unitOfWork.Repository<Course>().GetByID(manualExamDto.CourseID);
            if (course == null)
            {
                throw new KeyNotFoundException("The specified course does not exist.");
            }

            // Check if all question IDs exist
            foreach (var questionId in manualExamDto.QuestionsIDs)
            {
                var question =_unitOfWork.Repository<Question>().GetByID(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"The question with ID {questionId} does not exist.");
                }
            }

            var exam = _mapper.Map<Exam>(manualExamDto);
            if (exam == null)
            {
                throw new InvalidOperationException("Failed to map AutoExamDto to Exam.");
            }
            if (exam.ExamQuestions == null)
            {
                exam.ExamQuestions = new HashSet<ExamQuestion>();
            }

            exam.QuestionsNumber = manualExamDto.QuestionsIDs.Count;
            _examRepository.Add(exam);
            await _unitOfWork.CompleteAsync();

            foreach (var questionId in manualExamDto.QuestionsIDs)
            {
                var examQuestion = new ExamQuestion
                {
                    ExamID = exam.Id,
                    QuestionID = questionId
                };
                if (exam.ExamQuestions == null)
                {
                    exam.ExamQuestions = new HashSet<ExamQuestion>();
                }
                exam.ExamQuestions.Add(examQuestion);
            }

            await _unitOfWork.CompleteAsync();

            var examViewModel = _mapper.Map<ExamViewModel>(exam);
            examViewModel.QuestionsIDs = manualExamDto.QuestionsIDs;
            return examViewModel;
        }

        public async Task<ExamViewModel> GetExamById(int id)
        {
            var exam = _examRepository.GetByID(id);
            if (exam == null)
                return null;

            return _mapper.Map<ExamViewModel>(exam);
        }

        public async Task<ExamViewModel> GetExamForCourse(int courseId)
        {
            var exam = await _unitOfWork.Repository<Exam>().GetAll()
                           .Where(e => e.CourseID == courseId)
                           .FirstOrDefaultAsync();
            return _mapper.Map<ExamViewModel>(exam);
        }

        public async Task<bool> AssignStudentToExams(int examId, int studentId)
        {
            var studentExamRepo = _unitOfWork.Repository<StudentExam>();

            var exam = _unitOfWork.Repository<Exam>().GetByID(examId);
            if (exam == null) return false;

            //check that Instructor assigned a student to a course first, 
            var studentCourse = _unitOfWork.Repository<StudentCourse>().Get(sc => sc.StudentID == studentId && sc.CourseID == exam.CourseID);
            if (studentCourse.Count() == 0) return false;

            var studentExam = new StudentExam()
            {
                StudentID = studentId,
                ExamID = examId
            };
            studentExamRepo.Add(studentExam);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0;

        }
        public async Task<Exam> StartExam(int studentId, int examId)
        {
            var studentExamRepo = _unitOfWork.Repository<StudentExam>();
            var examRepo = _unitOfWork.Repository<Exam>();

            // Check that the instructor assigned a student to an exam first
            var studentExam = await studentExamRepo.GetAll()
                .FirstOrDefaultAsync(se => se.ExamID == examId && se.StudentID == studentId);

            if (studentExam == null) return null;

            // Load the exam with its questions and choices
            var exam = await examRepo.GetAll()
                .Include(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                    .ThenInclude(q =>q.choices)
                .FirstOrDefaultAsync(e => e.Id == examId);

            if (exam == null) return null;

            // Student can take many quiz exams, but only one final exam
            if (exam.Type == ExamType.Final && studentExam.IsCompleted)
            {
                var finalExams = await studentExamRepo.GetAll()
                    .Where(se => se.StudentID == studentId && se.Exam.Type == ExamType.Final)
                    .ToListAsync();

                if (finalExams.Count > 1) return null;
            }

            // Start the exam if not started
            if (!studentExam.StartedAt.HasValue)
            {
                studentExam.StartedAt = DateTime.UtcNow;
                studentExamRepo.Update(studentExam);
                await _unitOfWork.CompleteAsync();
            }

            return exam;
        }

        public async Task<bool> SubmitExam(SubmitExamDto submitExamDto)
        {
            var studentExamRepo = _unitOfWork.Repository<StudentExam>();
            var questionRepo = _unitOfWork.Repository<Question>();

            var studentExam = await studentExamRepo.GetAll()
                .Include(se => se.Exam)
                .Include(se => se.Exam.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                .ThenInclude(q => q.choices)
                .FirstOrDefaultAsync(se => se.ExamID == submitExamDto.ExamId && se.StudentID == submitExamDto.StudentId);

            if (studentExam == null || studentExam.IsCompleted) return false;

            // Evaluate the exam
            double totalScore = 0;
            foreach (var answer in submitExamDto.Answers)
            {
                var question = await questionRepo.GetAll()
                    .Include(q => q.choices)
                    .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

                if (question == null) continue;

                var correctChoice = question.choices.FirstOrDefault(c => c.IsRight);
                if (correctChoice != null && correctChoice.Id == answer.SelectedChoiceId)
                {
                    var examQuestion = studentExam.Exam.ExamQuestions.FirstOrDefault(eq => eq.QuestionID == question.Id);
                    if (examQuestion != null)
                    {
                        totalScore += examQuestion.Grade;
                    }
                }
            }

            // Save the results
            studentExam.IsCompleted = true;
            studentExam.SubmittedAt = DateTime.UtcNow;

            var studentExamResultRepo = _unitOfWork.Repository<StudentExamResult>();
            var result = new StudentExamResult
            {
                StudentID = submitExamDto.StudentId,
                ExamID = submitExamDto.ExamId,
                Score = totalScore,
                SubmittedAt = DateTime.UtcNow
            };
            studentExamResultRepo.Add(result);

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateAutoExam(int Id,AutoExamDto autoExamDto)
        {
            var exam = _examRepository.GetByID(Id);
            if (exam == null)
                return false;

            _mapper.Map(autoExamDto, exam);
            _examRepository.Update(exam);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateManualExam(int Id,ManualExamDto manualExamDto)
        {
            var exam =_examRepository.GetByID(Id);
            if (exam == null)
                return false;

            _mapper.Map(manualExamDto, exam);
            _examRepository.Update(exam);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        private IEnumerable<Question> SelectQuestionsForAutoExam(IEnumerable<Question> questions, int questionsNumber)
        {
            var easyQuestions = questions.Where(q => q.Level== QuestionLevel.Simple).ToList();
            var mediumQuestions = questions.Where(q => q.Level == QuestionLevel.Medium).ToList();
            var hardQuestions = questions.Where(q => q.Level== QuestionLevel.Hard).ToList();

            int easyCount = questionsNumber / 3;
            int mediumCount = questionsNumber / 3;
            int hardCount = questionsNumber / 3;

            int remainder = questionsNumber % 3;

            if (remainder == 1)
            {
                easyCount += 1;
            }
            else if (remainder == 2)
            {
                easyCount += 1;
                mediumCount += 1;
            }

            var selectedQuestions = new List<Question>();
            selectedQuestions.AddRange(easyQuestions.OrderBy(q => Guid.NewGuid()).Take(easyCount));
            selectedQuestions.AddRange(mediumQuestions.OrderBy(q => Guid.NewGuid()).Take(mediumCount));
            selectedQuestions.AddRange(hardQuestions.OrderBy(q => Guid.NewGuid()).Take(hardCount));

            return selectedQuestions;
        }

        public async Task<bool> DeleteExamAsync(int id)
        {
            var examRepo = _unitOfWork.Repository<Exam>();

            var exam = examRepo.GetByID(id);

            if (exam == null) return false;

            examRepo.Delete(exam);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0;
        }
    }
}
