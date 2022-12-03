using System.Collections;

namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class DishCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
