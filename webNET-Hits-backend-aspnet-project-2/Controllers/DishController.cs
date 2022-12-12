using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Helpers;
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

        [HttpGet]
        public async Task<ActionResult<DishPagedListDto>> Get([FromQuery] QueryParams queryParams)
        {
            FilterQueryParams filterQueryParams = _mapper.Map(queryParams, new FilterQueryParams());
            var response = await _dishService.GetListDishes(filterQueryParams);
            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<DishDto>> Get([FromRoute] Guid id)
        {
            var response = await _dishService.GetConcreteDish(id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id:Guid}/rating/check")]
        public async Task<ActionResult> Check([FromRoute] Guid id)
        {
            var response = await _dishService.CheckAbilityToSetRating(id, (Guid)HttpContext.Items["UserId"]);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("{id:Guid}/rating")]
        public async Task<ActionResult> Post([FromRoute] Guid id, [FromQuery][Range(0, 10)] int ratingScore)
        {
            await _dishService.SetRating(id, (Guid)HttpContext.Items["UserId"], ratingScore);
            return Ok();
        }
    }
}
