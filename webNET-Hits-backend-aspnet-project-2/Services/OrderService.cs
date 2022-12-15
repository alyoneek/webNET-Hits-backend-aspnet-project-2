using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Exceptions;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetConcreteOrder(Guid userId, Guid orderId);
        Task<IEnumerable<OrderInfoDto>> GetListOrders(Guid userId);
        Task CreateOrder(Guid userId, OrderCreateDto model);
        Task ConfirmOrderDelivery(Guid userId, Guid orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly DataBaseContext _context;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public OrderService(DataBaseContext context, IBasketService basketService, IMapper mapper)
        {
            _context = context;
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetConcreteOrder(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Dishes)
                .ThenInclude(d => d.Dish)
                .SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id = {orderId} doesn't exist in database");
            }

            if (order.UserId != userId)
            {
                throw new MismatchedValuesException($"User with id={userId} has insufficient rights");
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<IEnumerable<OrderInfoDto>> GetListOrders(Guid userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId).ToListAsync();

            var ordersDto = _mapper.Map<List<Order>, List<OrderInfoDto>>(orders);
            return ordersDto;
        }

        public async Task CreateOrder(Guid userId, OrderCreateDto model)
        {
            var dishesInCart = await _context.DishesInBasket
                .Include(d => d.Dish)
                .Where(d => d.CartId == userId && d.OrderId == null)
                .ToListAsync();

            if (dishesInCart.Count == 0)
            {
                throw new InvalidOperationException($"Empty basket for user with id={userId}");  //400
            }

            var price = GetTotalOrderPrice(dishesInCart);
            var order = _mapper.Map<Order>((userId, price, model));

            var addedOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            await _basketService.ExcludeOrdedDishes(userId, addedOrder.Entity.Id);
        }

        public async Task ConfirmOrderDelivery(Guid userId, Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id={orderId} doesn't exist in database");
            }

            if (order.UserId != userId)
            {
                throw new MismatchedValuesException($"User with id={userId} has insufficient rights");
            }

            if (order.Status == OrderStatus.Delivered)
            {
                throw new DublicateValueException($"Can't update status for order with id={orderId}");
            }

            order.Status = OrderStatus.Delivered;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        private decimal GetTotalOrderPrice(IEnumerable<DishInBasket> orderedDishes)
        {
            decimal totalPrice = orderedDishes.Sum(d => d.Dish.Price * d.Amount);
            return totalPrice;
        }
    }
}
