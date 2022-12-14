using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IOrderRepository
    {

    }
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataBaseContext _context;
        public OrderRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
