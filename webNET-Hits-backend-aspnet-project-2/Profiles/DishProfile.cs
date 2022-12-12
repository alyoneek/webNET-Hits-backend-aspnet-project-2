using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;

namespace webNET_Hits_backend_aspnet_project_2.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<DishDto, Dish>()
                .ForMember(dst => dst.Rating, opt => opt.Ignore())
                .ForMember(dst => dst.DishCategoryId, opt => opt.MapFrom(src => src.Category));

            CreateMap<Dish, DishDto>()
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.DishCategoryId));

            CreateMap<QueryParams, FilterQueryParams>()
                .ForMember(dest => dest.Categories, opt => opt.Condition(src => src.Categories != null))
                .ForMember(dest => dest.Page, opt => opt.Condition(src => src.Page != null))
                .ForMember(dest => dest.Vegetarian, opt => opt.Condition(src => src.Vegetarian != null));
        }
    }
}
