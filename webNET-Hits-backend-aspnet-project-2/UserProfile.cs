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
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;
        }
    }
}
