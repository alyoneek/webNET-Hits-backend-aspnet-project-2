namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class DishInBasket
    {
        public Guid CartId { get; set; }
        public Guid DishId { get; set; }
        public int Amount { get; set; } 
        public Guid? OrderId { get; set; }
        public Dish Dish { get; set; }
    }
}
