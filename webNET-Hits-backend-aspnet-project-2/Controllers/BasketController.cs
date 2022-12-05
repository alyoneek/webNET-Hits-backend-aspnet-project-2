using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Helpers;
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
        public IActionResult GetBasketInfo()
        {
            var response = _basketService.GetBasketInfo((Guid)HttpContext.Items["UserId"]);

            if (response == null)
            {
                return Forbid();
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("dish/{dishId:Guid}")]
        public async Task<IActionResult> AddDishToBasket([FromRoute] Guid dishId)
        {
            // странное
            var userId = (Guid)HttpContext.Items["UserId"];
            if (userId == null)
            {
                return Forbid();
            }

            var response = await _basketService.AddDishToBasket(userId, dishId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("dish/{dishId:Guid}")]
        public async Task<IActionResult> DeleteDishFromBasket([FromRoute] Guid dishId, [FromQuery] bool? increase)
        {
            var response = await _basketService.DeleteDishFromBasket((Guid)HttpContext.Items["UserId"], dishId, increase);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
