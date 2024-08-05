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
        public async Task<CourseUpdateViewModel> CreateCourse(CourseDTO course)
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

        public Task<bool> EnrollStudent(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseViewModel>> GetCoursesForInstructor(int instructorId)
        {
            var courses = _courseRepository.Get(c => c.InstructorID == instructorId);
            return courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<CourseUpdateViewModel> UpdateCourse(CourseUpdateViewModel course,int id)
        {
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
        }
    }
}
