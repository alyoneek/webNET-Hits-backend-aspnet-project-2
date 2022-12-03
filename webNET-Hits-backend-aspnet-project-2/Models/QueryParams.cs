using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class QueryParams
    {
        [Range(1, Int32.MaxValue)]
        public int? Page { get; set; }
        public  bool? Vegetarian { get; set; }
    }
}
