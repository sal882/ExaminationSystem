namespace ExaminationSystem.DTOs.Course
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public string Description { get; set; }
        public int InstructorId { get; set; }
        public int CreditHours { get; set; }
    }
}
