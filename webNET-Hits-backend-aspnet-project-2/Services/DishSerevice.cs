using AutoMapper;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Repositories;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IDishService
    {
        //Task<Dish> AddDish(DishDto model);
        Task<DishDto> GetConcreteDish(Guid dishId);
        Task<DishPagedListDto> GetListDishes(FilterQueryParams queryParams);
    }
    public class DishService : IDishService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public DishService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<Dish> AddDish(DishDto model)
        //{
        //    var dish = _mapper.Map<Dish>(model);
        //    var addedDish = await _dishRepository.Add(dish);

        //    return addedDish;
        //}

        public async Task<DishDto> GetConcreteDish(Guid dishId)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(d => d.Id == dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist.");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public async Task<DishPagedListDto> GetListDishes(FilterQueryParams queryParams)
        {
            var dishes = await _context.Dishes.ToListAsync();

            if (queryParams.Vegetarian)
            {
                dishes = dishes.Where(d => d.Vegetarian).ToList();
            }

            PageInfoModel pagination = new PageInfoModel(dishes.Count, queryParams.Page);  //todo

            if (queryParams.Page > pagination.Count)
            {
                throw new KeyNotFoundException("Invalid value for attribute page.");
            }

            dishes = queryParams.Sorting switch
            {
                DishSorting.NameAsc => dishes.OrderBy(d => d.Name).ToList(),
                DishSorting.NameDesc => dishes.OrderByDescending(d => d.Name).ToList(),
                DishSorting.PriceAsc => dishes.OrderBy(d => d.Price).ToList(),
                DishSorting.PriceDesc => dishes.OrderByDescending(d => d.Price).ToList(),
                DishSorting.RatingAsc => dishes.OrderBy(d => d.Rating).ToList(),
                DishSorting.RatingDesc => dishes.OrderByDescending(d => d.Rating).ToList(),
                _ => dishes.OrderBy(d => d.Name).ToList(),
            };
            var pagedDishes = dishes
                .Skip((pagination.Current - 1) * pagination.Size)
                .Take(pagination.Size)
                .ToList(); ;
            var pagedDishesDto = _mapper.Map<List<Dish>, List<DishDto>>(pagedDishes);

            return new DishPagedListDto(pagedDishesDto, pagination);
        }
    }
}
