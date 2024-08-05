using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationSystem.DTOs.Course;
using ExaminationSystem.Models;
using ExaminationSystem.Services.CourseService;
using ExaminationSystem.ViewModels.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService,IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CourseUpdateViewModel>> AddCourse(CourseUpdateViewModel model)
        {
            var course = await _courseService.CreateCourse(model);
            if (course is null)
                return BadRequest();
            return course;
        }
        [HttpPost("StudentName")]
        public async Task<ActionResult<bool>> EnrollStudent(int studentId, int courseId)
        {
            var EnrolledStudent = await _courseService.EnrollStudent(studentId, courseId);
            if (!EnrolledStudent)
                return BadRequest();
            else
                return Ok(EnrolledStudent);

        }
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get(int instructorID)
        {

            var courses = await _courseService.GetCoursesForInstructor(instructorID);
            return _mapper.ProjectTo<CourseViewModel>(courses.AsQueryable());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseUpdateViewModel>> Update(int id, CourseUpdateViewModel model )
        {
            var result = _courseService.UpdateCourse(model,id);
            if (result is null)
                return BadRequest();
            return await result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            return Ok(await _courseService.DeleteCourse(id));
        }
    }
}
