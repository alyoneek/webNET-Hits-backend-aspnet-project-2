using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IEfRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public TokenResponse Authenticate(LoginCredentials model)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) 
            {
                // todo: logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new TokenResponse(user, token);
        }

        public async Task<TokenResponse> Register(UserRegisterModel model)
        {
            var user = _mapper.Map<User>(model);

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
