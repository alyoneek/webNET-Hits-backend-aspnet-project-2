using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Packaging.Signing;
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
        private readonly IMapper _mapper;

        public DishController(IDishService dishService, IMapper mapper)
        {
            _dishService = dishService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> addDishes([FromBody]  IEnumerable<DishDto> collectionDishes)
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
        public IActionResult GetDishInfo([FromRoute] Guid id)
        {
            var response = _dishService.GetDishById(id);

            if (response == null)
            {
                return NotFound(new { Message = $"Dish with id={id} isn't in database" });
            }

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllDishes([FromQuery] QueryParams queryParams)
        {
            FilterQueryParams filterQueryParams = _mapper.Map(queryParams, new FilterQueryParams());
            var response = _dishService.GetAllDishesByParams(filterQueryParams);
            if (response == null)
            {
                return BadRequest(new { Message = "Invalid value for attribute page" });
            }
            return Ok(response);
        }
    }
}
