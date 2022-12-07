using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IOrderService
    {
        OrderDto GetOrderById(Guid orderId);
        IEnumerable<OrderInfoDto> GetOrders(Guid userId);
        Task<Order> CreateOrder(Guid userId, OrderCreateDto model);
        Task<Order> ConfirmOrder(Guid orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly IEfRepository<DishInBasket> _basketRepository;
        private readonly IEfRepository<Dish> _dishRepository;
        private readonly IEfRepository<Order> _ordersRepository;
        private readonly IMapper _mapper;

        public OrderService(IEfRepository<DishInBasket> basketRepository, IEfRepository<Dish> dishRepository, IEfRepository<Order> ordersRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _dishRepository = dishRepository;
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public OrderDto GetOrderById(Guid orderId)
        {
            // ОБЯЗАТЕЛЬНО ПРОВЕРИТЬ НА ПРАВА
            var dishes = _dishRepository.GetAll();
            var dishesInBasket = _basketRepository.GetAll();
            var order = _ordersRepository.GetById(orderId);

            if (order == null)
            {
                return null;
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public IEnumerable<OrderInfoDto> GetOrders(Guid userId)
        {
            // ОБЯЗАТЕЛЬНО ПРОВЕРИТЬ НА ПРАВА
            var orders = _ordersRepository.GetAll()
                .Where(o => o.UserId == userId).ToList();

            var ordersDto = _mapper.Map<List<Order>, List<OrderInfoDto>>(orders);
            return ordersDto;
        }

        public async Task<Order> CreateOrder(Guid userId, OrderCreateDto model)
        {
            var dishes = _dishRepository.GetAll();
            var dishesInCart = _basketRepository.GetAll().Where(d => d.CartId == userId && d.OrderId == null).ToList();

            if (dishesInCart.Count == 0)
            {
                return null;
            }

            var price = GetTotalOrderPrice(dishesInCart);

            var addedOrder = _mapper.Map<Order>((userId, price, model));
            var response = await _ordersRepository.Add(addedOrder);
            await EmptyBasket(dishesInCart, response.Id);

            return response;
        }

        public async Task<Order> ConfirmOrder(Guid orderId)
        {
            var order = _ordersRepository.GetById(orderId);
            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.Delivered;

            var result = await _ordersRepository.Edit(order);

            return result;
        }

        private decimal GetTotalOrderPrice(IEnumerable<DishInBasket> dishesInCart)
        {
            decimal totalPrice = dishesInCart.Sum(d => d.Dish.Price * d.Amount);
            return totalPrice;
        }

        private async Task EmptyBasket(List<DishInBasket> dishesInBasket, Guid orderId)
        {
            dishesInBasket.ForEach(d => d.OrderId = orderId);
            await _basketRepository.EditRange(dishesInBasket);
        }
    }
}
