using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
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

        //[HttpPost]
        //public async Task<IActionResult> addDishes([FromBody] IEnumerable<DishDto> collectionDishes)
        //{
        //    List<Dish> addedDishes = new List<Dish>();
        //    foreach (DishDto dish in collectionDishes)
        //    {
        //        var response = await _dishService.AddDish(dish);
        //        addedDishes.Add(response);
        //    }
        //    return Ok(addedDishes);
        //}

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<DishDto>> Get([FromRoute] Guid id)
        {
            try
            {
                var response = await _dishService.GetConcreteDish(id);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<DishPagedListDto>> Get([FromQuery] QueryParams queryParams)
        {
            try
            {
                FilterQueryParams filterQueryParams = _mapper.Map(queryParams, new FilterQueryParams());
                var response = await _dishService.GetListDishes(filterQueryParams);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }
    }
}
