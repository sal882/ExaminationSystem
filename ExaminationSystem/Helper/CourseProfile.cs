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
<<<<<<< HEAD
            CreateMap<CourseUpdateViewModel, Course>().ReverseMap();
=======
            CreateMap<CourseUpdateViewModel, Course>();
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, CourseViewModel>();
            CreateMap<Course, CourseViewModel>().ForMember(
                dst => dst.NumberOfExams,
                opt => opt.MapFrom(src => src.Exams.Count())
                ).ForMember(
                dst => dst.NumberOfStudents,
<<<<<<< HEAD
                opt => opt.MapFrom(src => src.StudentCourses.Count()));
            CreateMap<CourseViewModel, CourseViewModel>();

        }
=======
                opt => opt.MapFrom(src => src.StudentCourses.Count())); 

         }
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
