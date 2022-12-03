using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class FilterQueryParams
    {
        [Range(1, Int32.MaxValue)]
        public int Page { get; set; } = 1;
        public bool Vegetarian { get; set; } = false;
    }
}
