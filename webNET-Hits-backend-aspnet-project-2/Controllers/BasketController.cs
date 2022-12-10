using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var response = await _basketService.GetUserCartInfo((Guid)HttpContext.Items["UserId"]);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpPost("dish/{dishId:Guid}")]
        public async Task<IActionResult> Post([FromRoute] Guid dishId)
        {
            try
            {
                var userId = (Guid)HttpContext.Items["UserId"];
                await _basketService.AddDishToCart(userId, dishId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("dish/{dishId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid dishId, [FromQuery] bool? increase)
        {
            try
            {
                await _basketService.DeleteDishFromCart((Guid)HttpContext.Items["UserId"], dishId, increase);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }
    }
}
