using AutoMapper;
using ExaminationSystem.DTOs.Exam;
using ExaminationSystem.Models;
using ExaminationSystem.Services.ExamService;
using ExaminationSystem.ViewModels.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public ExamController(IExamService examService, IMapper mapper)
        {
            _examService = examService;
            _mapper = mapper;
        }

        [HttpPost("CreateAutoExam")]
        public async Task<ActionResult<ExamViewModel>> CreateAutoExam([FromBody] AutoExamDto autoExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var examViewModel = await _examService.CreateAutoExam(autoExamDto);

            if (examViewModel == null)
                return BadRequest("Failed to create auto exam.");

            return Ok(examViewModel);
        }

        [HttpPost("CreateManualExam")]
        public async Task<ActionResult<ExamViewModel>> CreateManualExam(ManualExamDto manualExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var examViewModel = await _examService.CreateManualExam(manualExamDto);

            if (examViewModel == null)
                return BadRequest("Failed to create manual exam.");

            return Ok(examViewModel);
        }
        [HttpPut("EditAutoExam/{id}")]
        public async Task<IActionResult> EditAutoExam(int id, [FromBody] AutoExamDto autoExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _examService.UpdateAutoExam(id,autoExamDto);

            if (!result)
                return BadRequest("Failed to update auto exam.");

            var updatedExam = await _examService.GetExamById(id);

            return Ok(updatedExam);
        }

        [HttpPut("EditManualExam/{id}")]
        public async Task<IActionResult> EditManualExam(int id, [FromBody] ManualExamDto manualExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _examService.UpdateManualExam(id,manualExamDto);

            if (!result)
                return BadRequest("Failed to update manual exam.");

            var updatedExam = await _examService.GetExamById(id);

            return Ok(updatedExam);
        }
        [HttpPost("Assign")]
        public async Task<ActionResult<bool>> AssignExamToStudent(int examId, int studentId)
        {
            var result = await _examService.AssignStudentToExams(examId, studentId);
            if (!result)
                return BadRequest();

            return Ok(result);

        }
        [HttpGet("StartExam")]
        public async Task<ActionResult<IEnumerable <ExamViewModel>>> TakeExam(int studentId, int examId)
        {
            var exam = await _examService.StartExam( studentId,  examId);
            if (exam == null)
                return NotFound();
            return Ok(_mapper.Map<ExamViewModel>(exam));
        }

        [HttpPost("submitExam")]
        public async Task<ActionResult> SubmitExam([FromBody] SubmitExamDto submitExamDto)
        {
            var result = await _examService.SubmitExam(submitExamDto);
            if (!result) return BadRequest("Submission failed or exam already submitted.");
            return Ok("Exam submitted successfully.");
        }

        // GET api/examParticipation/getExamForCourse
        [HttpGet("getExamForCourse/{courseId}")]
        public async Task<IActionResult> GetExamForCourse(int courseId)
        {
            try
            {
                var exam = await _examService.GetExamForCourse(courseId);
                if (exam == null)
                {
                    return NotFound("No exam found for the specified course.");
                }
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteExam(int id)
        {
            return Ok(await _examService.DeleteExamAsync(id));
        }
    }
}   

