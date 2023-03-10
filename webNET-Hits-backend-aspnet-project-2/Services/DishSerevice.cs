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
        public async Task<DishPagedListDto> GetListDishes(FilterQueryParams queryParams)
        {
            var dishes =  _context.Dishes.AsQueryable();
            var filteredDishes = await filterDishes(dishes, queryParams);
            var pagedDishesDto = _mapper.Map<List<Dish>, List<DishDto>>(filteredDishes);

            return new DishPagedListDto(pagedDishesDto, filteredDishes.Pagination);
        }
        public async Task<DishDto> GetConcreteDish(Guid dishId)
        {
            var dish = await _context.Dishes.FindAsync(dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public async Task<bool> CheckAbilityToSetRating(Guid dishId, Guid userId)
        {
            var dish = await _context.Dishes.FindAsync(dishId);

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            var dishDelivery = await _context.Orders
                .Include(o => o.Dishes)
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Delivered && o.Dishes.Any(d => d.DishId == dishId))
                .FirstOrDefaultAsync();

            if (dishDelivery == null)
            {
                return false;
            }

            return true;
        }
        public async Task SetRating(Guid dishId, Guid userId, int ratingScore)
        {
            var dish = await _context.Dishes
                .Include(d => d.Ratings)
                .Where(d => d.Id == dishId)
                .SingleOrDefaultAsync();

            if (dish == null)
            {
                throw new KeyNotFoundException($"Dish with id = {dishId} doesn't exist in database");
            }

            if (! await CheckAbilityToSetRating(dishId, userId))
            {
                throw new InvalidOperationException("User can't set rating on dish that wasn't ordered");  
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

            await CalculateTotalDishRating(dish);
        }

        private async Task<PaginatedList<Dish>> filterDishes(IQueryable<Dish> dishes, FilterQueryParams queryParams)
        {
            var cetegoriesArray = queryParams.Categories;
            dishes = dishes.Where(d => cetegoriesArray.Contains((DishCategoryType)d.DishCategoryId));

            if (queryParams.Vegetarian)
            {
                dishes = dishes.Where(d => d.Vegetarian);
            }

            PageInfoModel pagination = new PageInfoModel(await dishes.CountAsync(), queryParams.Page); 

            if (queryParams.Page > pagination.Count)
            {
                throw new ArgumentOutOfRangeException(null, "Invalid value for attribute page");    
            }

            dishes = queryParams.Sorting switch
            {
                DishSorting.NameAsc => dishes.OrderBy(d => d.Name),
                DishSorting.NameDesc => dishes.OrderByDescending(d => d.Name),
                DishSorting.PriceAsc => dishes.OrderBy(d => d.Price),
                DishSorting.PriceDesc => dishes.OrderByDescending(d => d.Price),
                DishSorting.RatingAsc => dishes.OrderBy(d => d.Rating.HasValue).ThenBy(d => d.Rating),
                DishSorting.RatingDesc => dishes.OrderByDescending(d => d.Rating.HasValue).ThenByDescending(d => d.Rating),
                _ => dishes.OrderBy(d => d.Name),
            };

            var pagedDishes = await PaginatedList<Dish>.CreateAsync(dishes, pagination);
            return pagedDishes;
        }

        private async Task CalculateTotalDishRating(Dish dish)
        {
            dish.Rating = dish.Ratings.ToList().Sum(r => r.RatingScore) / (double) dish.Ratings.ToList().Count;
            await _context.SaveChangesAsync();
        }
    }
}
