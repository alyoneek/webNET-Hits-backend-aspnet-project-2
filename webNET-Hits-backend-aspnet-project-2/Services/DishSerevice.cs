using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;
using System.Linq;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IDishService
    {
        Task<Dish> AddDish(DishDto model);
        DishDto GetDishById(Guid id);
        DishPagedListDto GetAllDishesByParams(FilterQueryParams queryParams);
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

        public DishDto GetDishById(Guid id)
        {
            var dish = _dishRepository.GetById(id);

            if (dish == null)
            {
                return null;
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public DishPagedListDto GetAllDishesByParams(FilterQueryParams queryParams)
        {
            var dishes = _dishRepository.GetAll();
            PageInfoModel pagination = new PageInfoModel(dishes.Count, queryParams.Page);
            var pagedDishes = dishes.Skip((pagination.Current - 1) * pagination.Size).Take(pagination.Size).ToList();
            List<DishDto> pagedDishesDto = new List<DishDto>();
            foreach (Dish dish in pagedDishes)
            {
                pagedDishesDto.Add(_mapper.Map<DishDto>(dish));
            }
            return new DishPagedListDto(pagedDishesDto, pagination);
        }
    }
}
