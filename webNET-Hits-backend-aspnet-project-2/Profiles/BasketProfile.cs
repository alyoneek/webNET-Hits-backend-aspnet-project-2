using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;

namespace webNET_Hits_backend_aspnet_project_2.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<DishInBasket, DishBasketDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.DishId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Dish.Name))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Dish.Price))
                .ForMember(dst => dst.TotalPrice, opt => opt.MapFrom(src => src.Dish.Price * src.Amount))
                .ForMember(dst => dst.Image, opt => opt.MapFrom(src => src.Dish.Image));
        }
    }
}
