using ExaminationSystem.Models;

namespace ExaminationSystem.Services.TokenService
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
