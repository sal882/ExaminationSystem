using ExaminationSystem.Models;
using ExaminationSystem.Services.TokenService;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(IUnitOfWork unitOfWork,ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<UserToReturnDto> Login(LoginDto loginDto)
        {
            var user = _unitOfWork.Repository<User>()
                                 .Get(u => u.Email == loginDto.Email).FirstOrDefault();
            if (user == null || user.Password != loginDto.Password)
                return null;

            return new UserToReturnDto()
            {
                Email = loginDto.Email,
                Token = await _tokenGenerator.GenerateToken(user)
            };
           
        }

        public async Task<UserToReturnDto> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                Role = registerDto.Role
            };

            var token = await _tokenGenerator.GenerateToken(user);
 
            if (user.Role.ToString().ToLower() == "student")
            {
                var student = new Student
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    Role =user.Role
                };
                _unitOfWork.Repository<Student>().Add(student);
            }
            else if (user.Role.ToString().ToLower() == "instructor")
            {
                var instructor = new Instructor
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    Role = user.Role
                };
                 _unitOfWork.Repository<Instructor>().Add(instructor);
            }
            await _unitOfWork.CompleteAsync();

            return new UserToReturnDto()
            {
                DisplayName = $"{user.FirstName} {user.LastName}",  
                Email = user.Email,
                Token = token
            };
        }
    }
}
