using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginCredentials model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var response = await _userService.Register(model);

            if (response == null)
            {
                return BadRequest(new { DuplicateUserName = $"Username {model.Email} is already taken." });
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetUserInfo()
        {
            var user = _userService.GetById(((User)HttpContext.Items["User"]).Id);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> ChangeUserInfo([FromBody] UserEditModel model)
        {
            var response = await _userService.Edit(model, ((User)HttpContext.Items["User"]).Id);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
