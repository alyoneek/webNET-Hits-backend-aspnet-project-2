using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class OrderCreateDto
    {
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        [MinLength(1)]
        public string Address { get; set; }
    }
}
