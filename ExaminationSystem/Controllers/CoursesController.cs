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
<<<<<<< HEAD
        public async Task<ActionResult<CourseUpdateViewModel>> AddCourse(CourseUpdateViewModel model)
=======
        public async Task<ActionResult<CourseUpdateViewModel>> AddCourse(CourseDTO model)
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        {
            var course = await _courseService.CreateCourse(model);
            if (course is null)
                return BadRequest();
            return course;
        }
<<<<<<< HEAD
        [HttpPost("StudentName")]
        public async Task<ActionResult<bool>> EnrollStudent(int studentId, int courseId)
        {
            var EnrolledStudent = await _courseService.EnrollStudent(studentId, courseId);
            if (!EnrolledStudent)
                return BadRequest();
            else
                return Ok(EnrolledStudent);

        }
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get(int instructorID)
        {

            var courses = await _courseService.GetCoursesForInstructor(instructorID);
<<<<<<< HEAD
            return _mapper.ProjectTo<CourseViewModel>(courses.AsQueryable());
=======
            return courses.AsQueryable()
                .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider);
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseUpdateViewModel>> Update(int id, CourseUpdateViewModel model )
        {
            var result = _courseService.UpdateCourse(model,id);
            if (result is null)
                return BadRequest();
            return await result;
        }
<<<<<<< HEAD
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            return Ok(await _courseService.DeleteCourse(id));
        }
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
