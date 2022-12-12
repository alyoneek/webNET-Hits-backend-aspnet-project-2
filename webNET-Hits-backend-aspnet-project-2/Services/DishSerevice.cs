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
using System.Drawing.Printing;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IDishService
    {
        //Task<Dish> AddDish(DishDto model);
        Task<DishPagedListDto> GetListDishes(FilterQueryParams queryParams);
        Task<DishDto> GetConcreteDish(Guid dishId);
        Task<bool> CheckAbilityToSetRating(Guid dishId, Guid userId);
        Task SetRating(Guid dishId, Guid userId, int ratingScore);
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
        public async Task<DishPagedListDto> GetListDishes(FilterQueryParams queryParams)
        {
            var dishes =  _context.Dishes.AsQueryable();
            var filteredDishes = await filterDishes(dishes, queryParams);
            var pagedDishesDto = _mapper.Map<List<Dish>, List<DishDto>>(filteredDishes);

            return new DishPagedListDto(pagedDishesDto, filteredDishes.Pagination);
        }
        public async Task<DishDto> GetConcreteDish(Guid dishId)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(d => d.Id == dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public async Task<bool> CheckAbilityToSetRating(Guid dishId, Guid userId)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(d => d.Id == dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            var dishDelivery = await _context.Orders
                .Include(o => o.Dishes)
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Delivered && o.Dishes.Any(d => d.DishId == dishId))
                .ToListAsync();

            if (dishDelivery.Count == 0)
            {
                return false;
            }

            return true;
        }
        public async Task SetRating(Guid dishId, Guid userId, int ratingScore)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(d => d.Id == dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            if (! await CheckAbilityToSetRating(dishId, userId))
            {
                //throw new KeyNotFoundException("User can't set rating on dish that wasn't ordered");   400
            }

            var existingRating = await _context.Ratings.SingleOrDefaultAsync(r => r.UserId == userId && r.DishId == dishId);

            if (existingRating == null)
            {
                await _context.Ratings.AddAsync(new Rating { DishId = dishId, UserId = userId, RatingScore = ratingScore });
            }      
            else
            {
                existingRating.RatingScore = ratingScore;
                _context.Ratings.Update(existingRating);
            }

            dish.Rating = dish.Ratings.ToList().Sum(r => r.RatingScore) / dish.Ratings.ToList().Count;
            await _context.SaveChangesAsync();
        }

        private async Task<PaginatedList<Dish>> filterDishes(IQueryable<Dish> dishes, FilterQueryParams queryParams)
        {
            if (queryParams.Vegetarian)
            {
                dishes = dishes.Where(d => d.Vegetarian);
            }

            PageInfoModel pagination = new PageInfoModel(await dishes.CountAsync(), queryParams.Page);  //todo

            if (queryParams.Page > pagination.Count)
            {
                //throw new KeyNotFoundException("Invalid value for attribute page.");    400
            }

            dishes = queryParams.Sorting switch
            {
                DishSorting.NameAsc => dishes.OrderBy(d => d.Name),
                DishSorting.NameDesc => dishes.OrderByDescending(d => d.Name),
                DishSorting.PriceAsc => dishes.OrderBy(d => d.Price),
                DishSorting.PriceDesc => dishes.OrderByDescending(d => d.Price),
                DishSorting.RatingAsc => dishes.OrderBy(d => d.Rating),
                DishSorting.RatingDesc => dishes.OrderByDescending(d => d.Rating),
                _ => dishes.OrderBy(d => d.Name),
            };

            var pagedDishes = await PaginatedList<Dish>.CreateAsync(dishes, pagination);
            return pagedDishes;
        }
    }
}
