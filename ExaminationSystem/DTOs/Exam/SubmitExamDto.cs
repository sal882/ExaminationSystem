namespace ExaminationSystem.DTOs.Exam
{
    public class SubmitExamDto
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

}
