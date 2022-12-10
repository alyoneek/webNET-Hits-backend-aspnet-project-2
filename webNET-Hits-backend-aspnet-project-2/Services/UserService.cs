using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IUserService
    {
        Task<TokenResponse> Login(LoginCredentials model);
        Task<TokenResponse> Register(UserRegisterModel userModel);
        Task EditUserProfile(UserEditModel userModel, Guid userId);
        Task<UserDto> GetUserProfile(Guid userId);
    }
    public class UserService : IUserService
    {
        private readonly DataBaseContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public UserService(DataBaseContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Login(LoginCredentials model)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser == null)
            {
                throw new KeyNotFoundException("Invalid login.");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, existingUser.Password);

            if (!isValidPassword) 
            {
                throw new KeyNotFoundException("Invalid passsword.");
            }

            var token = _jwtUtils.GenerateToken(existingUser);
            return new TokenResponse(token);
        }

        public async Task<TokenResponse> Register(UserRegisterModel model)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser != null)
            {
                throw new KeyNotFoundException($"Username {model.Email} is already taken.");
            }

            var registerUser = _mapper.Map<User>(model);
            await _context.Users.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            var response = await Login(new LoginCredentials
            {
                Email = model.Email,
                Password = model.Password,
            });

            return response;
        }

        public async Task EditUserProfile(UserEditModel model, Guid userId)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with id = {userId} doesn't exist.");
            }

            var newUser = _mapper.Map(model, existingUser);

            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserProfile(Guid userId) 
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with id = {userId} doesn't exist.");
            }

            var userDto = _mapper.Map<UserDto>(existingUser);
            return userDto;
        }
    }
}
