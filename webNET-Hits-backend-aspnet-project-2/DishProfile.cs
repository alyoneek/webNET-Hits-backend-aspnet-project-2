using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2
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
                .ForAllMembers(options => options.Condition((_,dst) => dst != null));
        }
    }
}
