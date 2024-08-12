using ExaminationSystem.DTOs.Course;
using ExaminationSystem.ViewModels.Course;

namespace ExaminationSystem.Services.CourseService
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetCoursesForInstructor(int instructorId);
<<<<<<< HEAD
        Task<CourseUpdateViewModel> CreateCourse(CourseUpdateViewModel course);
=======
        Task<CourseUpdateViewModel> CreateCourse(CourseDTO course);
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        Task<CourseUpdateViewModel> UpdateCourse(CourseUpdateViewModel course,int id);
        Task<bool> DeleteCourse(int courseId);
        Task<bool> EnrollStudent(int studentId, int courseId);
    }
}
