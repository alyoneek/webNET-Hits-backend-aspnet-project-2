using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class QueryParams
    {
        public List<DishCategoryType>? Categories { get; set; }
        public bool? Vegetarian { get; set; }
        public DishSorting? Sorting { get; set; }
        [Range(1, Int32.MaxValue)]
        public int? Page { get; set; }
    }
}
