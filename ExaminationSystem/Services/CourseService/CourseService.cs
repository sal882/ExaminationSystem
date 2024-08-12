using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationSystem.DTOs.Course;
using ExaminationSystem.Models;
using ExaminationSystem.Repositories;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.Course;

namespace ExaminationSystem.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IMapper mapper, 
            IGenericRepository<Course> courseRepository,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }
<<<<<<< HEAD
        public async Task<CourseUpdateViewModel> CreateCourse(CourseUpdateViewModel course)
=======
        public async Task<CourseUpdateViewModel> CreateCourse(CourseDTO course)
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        {
            var createdCourse = _mapper.Map<Course>(course);
            _courseRepository.Add(createdCourse);
            await _unitOfWork.CompleteAsync();
            var retrievedCourse = _courseRepository.GetByID(createdCourse.Id);
            if (retrievedCourse == null) return null;
            var instructor = _unitOfWork.Repository<Instructor>().GetByID(retrievedCourse.InstructorID);
            if (instructor == null) return null;
            retrievedCourse.Instructor = instructor;

            var result = _mapper.Map<CourseUpdateViewModel>(retrievedCourse);
            return result;
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var course = _courseRepository.GetByID(courseId);
            if(course != null)
            {
                _courseRepository.Delete(course);
                _courseRepository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

<<<<<<< HEAD
        public async Task<bool> EnrollStudent(int studentId, int courseId)
        {
            var studentCourseRepo = _unitOfWork.Repository<StudentCourse>();

            var studentCourse = new StudentCourse
            {
                StudentID = studentId,
                CourseID = courseId
            };

            studentCourseRepo.Add(studentCourse);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0;
=======
        public Task<bool> EnrollStudent(int studentId, int courseId)
        {
            throw new NotImplementedException();
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        }

        public async Task<IEnumerable<CourseViewModel>> GetCoursesForInstructor(int instructorId)
        {
<<<<<<< HEAD
            var courses = _unitOfWork.Repository<Course>()
                                   .Get(c => c.InstructorID == instructorId);
            return _mapper.Map<IEnumerable<CourseViewModel>>(courses);
=======
            var courses = _courseRepository.Get(c => c.InstructorID == instructorId);
            return courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToList();
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        }

        public async Task<CourseUpdateViewModel> UpdateCourse(CourseUpdateViewModel course,int id)
        {
<<<<<<< HEAD
            var courseRepo = _unitOfWork.Repository<Course>();

            var existingCourse = courseRepo.GetByID(id);

            if (existingCourse == null) return null;

            existingCourse.Name = course.Name;
            existingCourse.CreditHours = course.CreditHours;
            existingCourse.InstructorID = course.InstructorId;

            courseRepo.Update(existingCourse);
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return course;
=======
            var Course = _courseRepository.GetByID(id);
            if (Course.InstructorID != course.InstructorId) return null;
            else
            {
                var Updatedcourse = _mapper.Map<Course>(course);
                if (course != null)
                {
                    course.CreditHours = Updatedcourse.CreditHours;
                    course.Name = Updatedcourse.Name;
                    _courseRepository.Update(Course);
                    _courseRepository.SaveChanges();
                    var retrievedCourse = _mapper.Map<CourseUpdateViewModel>(Updatedcourse);
                    return retrievedCourse ;
                }
                return null;
            }
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        }
    }
}
