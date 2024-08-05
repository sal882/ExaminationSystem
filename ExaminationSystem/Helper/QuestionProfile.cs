using AutoMapper;
using ExaminationSystem.DTOs.QuestionDto;
using ExaminationSystem.Models;
using ExaminationSystem.ViewModels.Question;

namespace ExaminationSystem.Helper
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>()
              .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.ToString()))
              .ForMember(dest => dest.choices, opt => opt.MapFrom(src => src.choices.ToDictionary(c => c.Text, c => c.IsRight)));

             


            // Mapping from QuestionViewModel to Question entity and vice versa
            CreateMap<Question, QuestionViewModel>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.ToString())) // Converts the enum to its name
            .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.choices));
            CreateMap<QuestionViewModel, Question>();

            // Mapping for choices (if required)
            CreateMap<Choice, ChoiceDto>().ReverseMap();
        }
    }
}
