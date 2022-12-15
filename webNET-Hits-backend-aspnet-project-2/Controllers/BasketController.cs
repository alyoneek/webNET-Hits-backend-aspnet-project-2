using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishBasketDto>>> Get()
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            var response = await _basketService.GetUserCartInfo(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("dish/{dishId:Guid}")]
        public async Task<ActionResult> Post([FromRoute] Guid dishId)
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            await _basketService.AddDishToCart(userId, dishId);
            return Ok();
        }

        [Authorize]
        [HttpDelete("dish/{dishId:Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid dishId, [FromQuery] bool? increase)
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            await _basketService.DeleteDishFromCart(userId, dishId, increase);
            return Ok();
        }
    }
}
