using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using webNET_Hits_backend_aspnet_project_2.Attributes;
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

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Post([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.Register(model);
            return Ok(response);


        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Post([FromBody] LoginCredentials model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.Login(model);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Post()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _userService.Logout(token);
            return Ok(new { message = "Logged Out" });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> Get()
        {
            var response = await _userService.GetUserProfile((Guid)HttpContext.Items["UserId"]);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<ActionResult> Put([FromBody] UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.EditUserProfile(model, (Guid)HttpContext.Items["UserId"]);
            return Ok();
        }
    }
}
