using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IOrderService
    {
        OrderDto GetOrderById(Guid orderId);
        Task<Order> CreateOrder(Guid userId, OrderCreateDto model);
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
