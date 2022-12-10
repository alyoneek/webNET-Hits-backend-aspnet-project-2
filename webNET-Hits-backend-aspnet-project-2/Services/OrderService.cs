using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetConcreteOrder(Guid orderId);
        Task<IEnumerable<OrderInfoDto>> GetListOrders(Guid userId);
        Task CreateOrder(Guid userId, OrderCreateDto model);
        Task ConfirmOrderDelivery(Guid orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public OrderService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetConcreteOrder(Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Dishes)
                .ThenInclude(d => d.Dish)
                .SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id = {orderId} doesn't exist.");
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
                .Where(d => d.CartId == userId && d.OrderId == null).ToListAsync();

            if (dishesInCart.Count == 0)
            {
                throw new KeyNotFoundException($"Empty basket for user with id={userId}");
            }

            var price = GetTotalOrderPrice(dishesInCart);
            var order = _mapper.Map<Order>((userId, price, model));

            var addedOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            await EmptyBasket(dishesInCart, addedOrder.Entity.Id);
        }

        public async Task ConfirmOrderDelivery(Guid orderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id={orderId} doesn't exist.");
            }
            order.Status = OrderStatus.Delivered;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        private decimal GetTotalOrderPrice(IEnumerable<DishInBasket> dishesInCart)
        {
            decimal totalPrice = dishesInCart.Sum(d => d.Dish.Price * d.Amount);
            return totalPrice;
        }

        private async Task EmptyBasket(List<DishInBasket> dishesInBasket, Guid orderId)
        {
            dishesInBasket.ForEach(d => d.OrderId = orderId);
            _context.DishesInBasket.UpdateRange(dishesInBasket);
            await _context.SaveChangesAsync();
        }
    }
}
