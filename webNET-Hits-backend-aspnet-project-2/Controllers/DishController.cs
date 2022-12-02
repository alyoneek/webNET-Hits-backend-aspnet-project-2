using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]
        public async Task<IActionResult> addDishes(IEnumerable<DishDto> collectionDishes)
        {
            List<Dish> addedDishes = new List<Dish>();
             foreach (DishDto dish in collectionDishes)
            {
                var response = await _dishService.AddDish(dish);
                addedDishes.Add(response);
            }
            return Ok(addedDishes);
        }
    }
}
