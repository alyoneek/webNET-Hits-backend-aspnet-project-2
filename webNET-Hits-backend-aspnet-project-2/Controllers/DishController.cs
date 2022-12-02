using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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

        [HttpGet("{id:Guid}")]
        public IActionResult GetDishInfo(Guid id)
        {
            var dish = _dishService.GetDishById(id);

            if (dish == null)
            {
                return NotFound(new { Message= $"Dish with id={id} isn't in database" });
            }

            return Ok(dish);
        }
    }
}
