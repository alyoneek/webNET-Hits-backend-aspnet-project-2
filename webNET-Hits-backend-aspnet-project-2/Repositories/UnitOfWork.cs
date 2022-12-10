namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IDishRepository Dishes { get; }
        IBasketRepository Basket { get; }
        IOrderRepository Orders { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IDishRepository Dishes { get; private set; }
        public IBasketRepository Basket { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public UnitOfWork(DataBaseContext context, IUserRepository users, 
            IDishRepository dishes, IBasketRepository basket, IOrderRepository orders)
        {
            Users = users;
            Dishes = dishes;
            Basket = basket;
            Orders = orders;
        }
    }
}
