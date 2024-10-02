using ExaminationSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ExaminationSystem.Models;
using ExaminationSystem.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ExaminationSystem.Services.TokenService
{
    public class TokenGenerator : ITokenGenerator
    {
        public async Task<string> GenerateToken(User user)
        {
            var roleString = user.Role.ToString();

            // Create a ClaimsIdentity with the user's data
            var claims = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleString)
            });

            // Define the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "MySecuredAPIsUser",
                Audience = "Instructors_Student",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token handler and the security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            // Write the token as a string
            var tokenString = tokenHandler.WriteToken(securityToken);

            return await Task.FromResult(tokenString);
        }
    }
}
