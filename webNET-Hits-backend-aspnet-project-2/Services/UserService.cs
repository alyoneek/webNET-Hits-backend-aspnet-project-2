﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using webNET_Hits_backend_aspnet_project_2.Exceptions;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IUserService
    {
        Task<TokenResponse> Register(UserRegisterModel userModel);
        Task<TokenResponse> Login(LoginCredentials model);
        Task Logout(string token);
        Task<UserDto> GetUserProfile(Guid userId);
        Task EditUserProfile(UserEditModel userModel, Guid userId);
    }
    public class UserService : IUserService
    {
        private readonly DataBaseContext _context;
        private readonly IDistributedCache _cache;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public UserService(DataBaseContext context, IDistributedCache cache, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _cache = cache;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Register(UserRegisterModel model)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser != null)
            {
                throw new DublicateValueException($"Email {model.Email} is already taken"); //400
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
        public async Task<TokenResponse> Login(LoginCredentials model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                throw new FailedAuthorizationException("Invalid login");  
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!isValidPassword) 
            {
                throw new FailedAuthorizationException("Invalid passsword");  
            }

            var token = _jwtUtils.GenerateToken(user);
            return new TokenResponse(token);
        }

        public async Task Logout(string token)
        {
            await _cache.SetStringAsync(token, " ", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
        }
        public async Task<UserDto> GetUserProfile(Guid userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                //throw new KeyNotFoundException($"User with id = {userId} doesn't exist in database");   ?
            }

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task EditUserProfile(UserEditModel model, Guid userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                //throw new KeyNotFoundException($"User with id = {userId} doesn't exist in database"); ?
            }

            var newUser = _mapper.Map(model, user);

            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
