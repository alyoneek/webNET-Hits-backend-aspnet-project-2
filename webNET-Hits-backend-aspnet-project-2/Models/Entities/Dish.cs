namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Boolean Vegetarian { get; set; }
        public double? Rating { get; set; }
        public int DishCategoryId { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
