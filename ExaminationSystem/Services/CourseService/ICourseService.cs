using ExaminationSystem.DTOs.Course;
using ExaminationSystem.ViewModels.Course;

namespace ExaminationSystem.Services.CourseService
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetCoursesForInstructor(int instructorId);
 
        Task<CourseUpdateViewModel> CreateCourse(CourseUpdateViewModel course);
 
        Task<CourseUpdateViewModel> UpdateCourse(CourseUpdateViewModel course,int id);
        Task<bool> DeleteCourse(int courseId);
        Task<bool> EnrollStudent(int studentId, int courseId);
    }
}
