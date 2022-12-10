using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        List<Dish> GetDishesByParams(FilterQueryParams queryParams, PageInfoModel pagination);
    }
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly DataBaseContext _context;
        public DishRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public List<Dish> GetDishesByParams(FilterQueryParams queryParams, PageInfoModel pagination)
        {
            var dishes = GetAll();
            if (queryParams.Vegetarian)
            {
                dishes = _context.Dishes.Where(d => d.Vegetarian).ToList();
            }
            return dishes.Skip((pagination.Current - 1) * pagination.Size).Take(pagination.Size).ToList();
        }
    }
}
