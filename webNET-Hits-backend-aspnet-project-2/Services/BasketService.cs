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
            var dishesInBasket = _basketRepository.GetAll().Where(d => d.CartId == userId).ToList();

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
                .SingleOrDefault(c => c.CartId == userId && c.DishId == dishId);

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
    }
}
