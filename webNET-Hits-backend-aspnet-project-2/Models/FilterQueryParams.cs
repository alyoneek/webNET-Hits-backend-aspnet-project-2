using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class FilterQueryParams
    {
        public bool Vegetarian { get; set; } = false;
        public DishSorting Sorting { get; set; } = DishSorting.NameAsc;
        [Range(1, Int32.MaxValue)]
        public int Page { get; set; } = 1;
    }
}
