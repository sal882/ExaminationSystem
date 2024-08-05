using AutoMapper;
using ExaminationSystem.DTOs.Exam;
using ExaminationSystem.Models;
using ExaminationSystem.ViewModels.Exam;

namespace ExaminationSystem.Helper
{
    public class ExamProfile:Profile
    {
        public ExamProfile()
        {
            CreateMap<AutoExamDto,Exam>();
            CreateMap<ManualExamDto, Exam>();
            CreateMap<Exam, ExamViewModel>()
                 .ForMember(dest => dest.QuestionsIDs, opt => opt
                 .MapFrom(src => src.ExamQuestions.Select(eq => eq.QuestionID).ToList()));
        }
    }
}
