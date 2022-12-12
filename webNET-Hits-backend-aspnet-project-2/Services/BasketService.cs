using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<DishBasketDto>> GetUserCartInfo(Guid userId);
        Task AddDishToCart(Guid userId, Guid dishId);
        Task DeleteDishFromCart(Guid userId, Guid dishId, bool? increase);
        Task ExcludeOrdedDishes(Guid userId, Guid orderId);
    }


    public class BasketService : IBasketService
    {

        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public BasketService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DishBasketDto>> GetUserCartInfo(Guid userId)
        {
            var dishesInBasket = await _context.DishesInBasket
                .Include(d => d.Dish)
                .Where(d => d.CartId == userId && d.OrderId == null)
                .ToListAsync();
            var dishesInBasketDto = _mapper.Map<List<DishInBasket>, List<DishBasketDto>>(dishesInBasket);

            return dishesInBasketDto;
        }

        public async Task AddDishToCart(Guid userId, Guid dishId)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(d => d.Id == dishId);
            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            var cartItem = await _context.DishesInBasket
                .SingleOrDefaultAsync(c => c.CartId == userId && c.DishId == dishId && c.OrderId == null); 

            if (cartItem == null)
            {
                cartItem = new DishInBasket
                {
                    DishId = dishId,
                    CartId = userId,
                    Amount = 1,
                };

                await _context.DishesInBasket.AddAsync(cartItem);
            }
            else
            {
                cartItem.Amount++;
                _context.DishesInBasket.Update(cartItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDishFromCart(Guid userId, Guid dishId, bool? increase)
        {
            var cartItem = await _context.DishesInBasket
                .SingleOrDefaultAsync(c => c.CartId == userId && c.DishId == dishId && c.OrderId == null);

            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} isn't in basket");
            }

            if (increase == null || increase == false || increase == true && cartItem.Amount == 1)
            {
                _context.DishesInBasket.Remove(cartItem);
            } 
            else
            {
                cartItem.Amount--;
                _context.DishesInBasket.Update(cartItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task ExcludeOrdedDishes(Guid userId, Guid orderId)
        {
            var dishesInCart = await _context.DishesInBasket
                .Include(d => d.Dish)
                .Where(d => d.CartId == userId && d.OrderId == null)
                .ToListAsync();

            dishesInCart.ForEach(d => d.OrderId = orderId);
            _context.DishesInBasket.UpdateRange(dishesInCart);
            await _context.SaveChangesAsync();
        }
    }
}
