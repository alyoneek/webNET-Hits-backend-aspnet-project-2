using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
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
        public async Task<ActionResult<TokenResponse>> Post([FromBody] LoginCredentials model)
        {
            try
            {
                var response = await _userService.Login(model);
                return Ok(response);
            }
            catch(KeyNotFoundException e) 
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Post([FromBody] UserRegisterModel model)
        {
            try
            {
                var response = await _userService.Register(model);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> Get()
        {
            try
            {
                var response = await _userService.GetUserProfile((Guid)HttpContext.Items["UserId"]);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> Put([FromBody] UserEditModel model)
        {
            try
            {
                await _userService.EditUserProfile(model, (Guid)HttpContext.Items["UserId"]);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }
    }
}
