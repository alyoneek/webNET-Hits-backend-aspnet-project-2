using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2
{
    public class UserProfile : Profile
    {

        public UserProfile() 
        {
            CreateMap<UserRegisterModel, User>()
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<UserEditModel, User>(); 

            CreateMap<User, UserDto>();
        }
    }
}
