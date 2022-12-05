using AutoMapper;
using System.Collections.Generic;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IBasketService
    {
        IEnumerable<DishBasketDto> GetBasketInfo(Guid userId);
        Task<DishInBasket> AddDishToBasket(Guid userId, Guid dishId);
        Task<DishInBasket> DeleteDishFromBasket(Guid userId, Guid dishId, bool? increase);
    }


    public class BasketService : IBasketService
    {

        private readonly IEfRepository<DishInBasket> _basketRepository;
        private readonly IEfRepository<Dish> _dishRepository;
        private readonly IMapper _mapper;

        public BasketService(IEfRepository<DishInBasket> basketRepository, IEfRepository<Dish> dishRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public IEnumerable<DishBasketDto> GetBasketInfo(Guid userId)
        {
            // нет проверки юзера
            // подтягивание кринге
            var dishes = _dishRepository.GetAll();
            var dishesInBasket = _basketRepository.GetAll().Where(d => d.CartId == userId && d.OrderId == null).ToList();

            var dishesInBasketDto = _mapper.Map<List<DishInBasket>, List<DishBasketDto>>(dishesInBasket);

            return dishesInBasketDto;
        }

        public async Task<DishInBasket> AddDishToBasket(Guid userId, Guid dishId)
        {
            var dish = _dishRepository.GetById(dishId);
            if (dish == null)
            {
                return null;
            }

            var cartItem = _basketRepository.GetAll()
                .SingleOrDefault(c => c.CartId == userId && c.DishId == dishId && c.OrderId == null);

            DishInBasket addedDish;

            if (cartItem == null)
            {
                cartItem = new DishInBasket
                {
                    DishId = dishId,
                    CartId = userId,
                    Amount = 1,
                    Dish = dish,
                };

                addedDish = await _basketRepository.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
                addedDish = await _basketRepository.Edit(cartItem);
            }

            return addedDish;
        }

        public async Task<DishInBasket> DeleteDishFromBasket(Guid userId, Guid dishId, bool? increase)
        {
            var dish = _basketRepository.GetAll()
                .SingleOrDefault(d => d.CartId == userId && d.DishId == dishId && d.OrderId == null);

            if (dish == null)
            {
                return null;
            }

            DishInBasket removed;

            if (increase == null || increase == false || increase == true && dish.Amount == 1)
            {
                removed = await _basketRepository.Delete(dish);
            } 
            else
            {
                dish.Amount--;
                removed = await _basketRepository.Edit(dish);
            }

            return removed; 
        }

        //public void EmptyCart()
        //{
        //    ShoppingCartId = GetCartId();
        //    var cartItems = _db.ShoppingCartItems.Where(
        //        c => c.CartId == ShoppingCartId);
        //    foreach (var cartItem in cartItems)
        //    {
        //        _db.ShoppingCartItems.Remove(cartItem);
        //    }           
        //    _db.SaveChanges();
        //}
    }
}
