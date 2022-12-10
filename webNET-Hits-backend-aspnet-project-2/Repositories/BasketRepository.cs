using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IBasketRepository : IGenericRepository<DishInBasket>
    {
        List<DishInBasket> GetUserDishes(Guid userId);
        DishInBasket GetUserConcreteDish(Guid userId, Guid dishId);
    }
    public class BasketRepository : GenericRepository<DishInBasket>, IBasketRepository
    {
        private readonly DataBaseContext _context;
        public BasketRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public List<DishInBasket> GetUserDishes(Guid userId)
        {
            return _context.DishesInBasket.Include(d => d.Dish).Where(d => d.CartId == userId && d.OrderId == null).ToList();
        }
        public DishInBasket GetUserConcreteDish(Guid userId, Guid dishId)
        {
            return _context.DishesInBasket.Include(d => d.Dish).SingleOrDefault(c => c.CartId == userId && c.DishId == dishId && c.OrderId == null);
        }
    }
}
