using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using webNET_Hits_backend_aspnet_project_2.Helpers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DtoModels;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet("{id:Guid}")]
        public IActionResult GetOrderById([FromRoute] Guid id)
        {
            //var userId = (Guid)HttpContext.Items["UserId"];
            var response = _orderService.GetOrderById(id);
            if (response == null)
            {
                return NotFound(new Response("error", $"Order with id={id} doesn't exist"));
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            var response = _orderService.GetOrders(userId);
            //if (response == null)
            //{
            //    return NotFound(new Response("error", $"Order with id={id} doesn't exist"));
            //}
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto model)
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            var response = await _orderService.CreateOrder(userId, model);
            if (response == null)
            {
                return BadRequest(new Response("error", $"Empty basket for user with id={userId}"));
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("{id:Guid}/status")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] Guid id)
        {
            //var userId = (Guid)HttpContext.Items["UserId"];
            var response = await _orderService.ConfirmOrder(id);
            if (response == null)
            {
                return NotFound(new Response("error", $"Order with id={id} doesn't exist."));
            }
            return Ok(response);
        }
    }
}
