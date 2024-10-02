using ExaminationSystem.Services.UserService;
using ExaminationSystem.UnitOfWork;
using ExaminationSystem.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
 
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserService userService,IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserToReturnDto>>Login(LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto);
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserToReturnDto>>Register(RegisterDto registerDto)
        {
            var user = await _userService.Register(registerDto);
            if (user == null)
                return Unauthorized();
            return Ok(user);
        }
    }
}
