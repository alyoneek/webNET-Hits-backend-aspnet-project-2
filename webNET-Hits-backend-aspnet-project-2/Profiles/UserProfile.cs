using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterModel, User>()
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null ? FormatPhoneNumber(src.PhoneNumber) : null))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

            CreateMap<UserEditModel, User>()
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null ? FormatPhoneNumber(src.PhoneNumber) : null));

            CreateMap<User, UserDto>();
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            string cleanedNumber = Regex.Replace(phoneNumber, @"[^\d\+]", "");
                //phoneNumber.Replace("(", "");
            //cleaned = phoneNumber.Replace(")", "");
            //cleaned = phoneNumber.Replace("-", "");
            //cleaned = phoneNumber.Replace(" ", "");

            MatchCollection match = Regex.Matches(cleanedNumber, @"^(\+7)(\d{3})(\d{3})(\d{2})(\d{2})$");
            string convertedNumber = match[0].Groups[1].ToString() + " (" + match[0].Groups[2].ToString() + ") " +
                match[0].Groups[3].ToString() + "-" + match[0].Groups[4].ToString() + "-" + match[0].Groups[5].ToString();
            return convertedNumber;
        }
    }
}
