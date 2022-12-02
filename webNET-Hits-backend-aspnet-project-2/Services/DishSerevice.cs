using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IDishService
    {
        Task<Dish> AddDish(DishDto model);
    }
    public class DishService : IDishService
    {
        private readonly IEfRepository<Dish> _dishRepository;
        private readonly IMapper _mapper;
        public DishService(IEfRepository<Dish> dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<Dish> AddDish(DishDto model)
        {
            var dish = _mapper.Map<Dish>(model);

            var addedDish = await _dishRepository.Add(dish);

            return addedDish;
        }
    }
}
