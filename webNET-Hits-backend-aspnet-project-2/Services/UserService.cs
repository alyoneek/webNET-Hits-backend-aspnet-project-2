using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IUserService
    {
        TokenResponse Authenticate(LoginCredentials model);
        Task<TokenResponse> Register(UserRegisterModel userModel);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public UserService(IEfRepository<User> userRepository, IJwtUtils jwtUtils, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public TokenResponse Authenticate(LoginCredentials model)
        {
            var user = _userRepository
                .GetAll()
                .SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) 
            {
                // todo: logger
                return null;
            }

            var token = _jwtUtils.GenerateToken(user);

            return new TokenResponse(token);
        }

        public async Task<TokenResponse> Register(UserRegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            var existedUser = _userRepository
                .GetAll()
                .SingleOrDefault(x => x.Email == user.Email);

            if (existedUser != null)
            {
                // todo: logger
                return null;
            }

            var addedUser = await _userRepository.Add(user);

            var response = Authenticate(new LoginCredentials
            {
               Email = user.Email,
               Password = user.Password,  
            });

            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(Guid id) 
        {
            return _userRepository.GetById(id);
        }
    }
}
