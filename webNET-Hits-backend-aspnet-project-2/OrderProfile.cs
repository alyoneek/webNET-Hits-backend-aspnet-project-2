using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<(Guid, decimal, OrderCreateDto), Order>()
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.Item1))
                .ForMember(dst => dst.DeliveryTime, opt => opt.MapFrom(src => src.Item3.DeliveryTime))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Item3.Address));

            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderInfoDto>();
        }
    }
}
