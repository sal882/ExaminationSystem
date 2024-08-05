using AutoMapper;
using ExaminationSystem.Services.ExamService;
using ExaminationSystem.Services.ResultService;
using ExaminationSystem.ViewModels.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
    public class ResultController : BaseController
    {
        private readonly IResultService _resultService;
        private readonly IMapper _mapper;

        public ResultController(IResultService resultService, IMapper mapper)
        {
            _resultService = resultService;
            _mapper = mapper;
        }
        [HttpGet("viewResult")]
        public async Task<ActionResult<ExamResultViewModel>> ViewResult(int studentId, int examId)
        {
            var result = await _resultService.GetStudentResult(studentId, examId);
            if (result == null) return NotFound("Result not found.");
            return Ok(result);
        }
        [HttpGet("viewAllResults")]
        public async Task<ActionResult<IEnumerable<ExamResultViewModel>>> ViewAllResults(int examId)
        {
            var results = await _resultService.GetAllResultsForExam(examId);
            return Ok(results);
        }
    }
}
