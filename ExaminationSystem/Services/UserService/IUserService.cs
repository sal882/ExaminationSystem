using ExaminationSystem.ViewModels.User;

namespace ExaminationSystem.Services.UserService
{
    public interface IUserService
    {
        Task<UserToReturnDto> Login(LoginDto loginDto);
        Task<UserToReturnDto> Register(RegisterDto registerDto);
    }
}
