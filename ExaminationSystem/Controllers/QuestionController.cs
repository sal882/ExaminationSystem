using AutoMapper;
using ExaminationSystem.DTOs.QuestionDto;
using ExaminationSystem.Models;
using ExaminationSystem.Services.QuestionService;
using ExaminationSystem.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper
            ,IQuestionService questionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionService = questionService;
        }
        // GET: api/Question/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateQuestion([FromBody]QuestionDto createQuestionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var question = await _questionService.CreateQuestionAsync(createQuestionDto);
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }
        // PUT: api/Question/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _questionService.UpdateQuestionAsync(questionDto,id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
        // DELETE: api/Question/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/Questions/AssignGrade
        [HttpPost("AssignGrade")]
        public async Task<IActionResult> AssignGrade([FromBody] AssignGradeDto assignGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _questionService.AssignGradeToQuestionAsync(assignGradeDto.Grade, assignGradeDto.QuestionId, assignGradeDto.Grade);
                return Ok("Grade assigned successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }


}

