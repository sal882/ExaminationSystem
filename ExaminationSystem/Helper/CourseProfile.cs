using AutoMapper;
using ExaminationSystem.DTOs.Course;
using ExaminationSystem.Models;
using ExaminationSystem.ViewModels.Course;
using System.Security.Cryptography;

namespace ExaminationSystem.Helper
{
    public class CourseProfile:Profile
    {
         public CourseProfile()
         {
            CreateMap<CourseUpdateViewModel, CourseDTO>();
            CreateMap<CourseUpdateViewModel, Course>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, CourseViewModel>();
            CreateMap<Course, CourseViewModel>().ForMember(
                dst => dst.NumberOfExams,
                opt => opt.MapFrom(src => src.Exams.Count())
                ).ForMember(
                dst => dst.NumberOfStudents,
                opt => opt.MapFrom(src => src.StudentCourses.Count())); 

         }
    }
}
