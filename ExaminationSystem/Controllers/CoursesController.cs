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
        public async Task<ActionResult<CourseUpdateViewModel>> AddCourse(CourseDTO model)
        {
            var course = await _courseService.CreateCourse(model);
            if (course is null)
                return BadRequest();
            return course;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get(int instructorID)
        {

            var courses = await _courseService.GetCoursesForInstructor(instructorID);
            return courses.AsQueryable()
                .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseUpdateViewModel>> Update(int id, CourseUpdateViewModel model )
        {
            var result = _courseService.UpdateCourse(model,id);
            if (result is null)
                return BadRequest();
            return await result;
        }
    }
}
