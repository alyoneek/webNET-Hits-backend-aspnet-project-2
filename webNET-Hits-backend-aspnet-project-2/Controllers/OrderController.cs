using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<OrderDto>> Get([FromRoute] Guid id)
        {
            try
            {
                var response = await _orderService.GetConcreteOrder(id);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderInfoDto>>> Get()
        {
            try
            {
                var userId = (Guid)HttpContext.Items["UserId"];
                var response = await _orderService.GetListOrders(userId);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto model)
        {
            try
            {
                var userId = (Guid)HttpContext.Items["UserId"];
                await _orderService.CreateOrder(userId, model);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }

        [Authorize]
        [HttpPost("{id:Guid}/status")]
        public async Task<IActionResult> Post([FromRoute] Guid id)
        {
            try
            {
                await _orderService.ConfirmOrderDelivery(id);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new Response { Message = e.Message });
            }
        }
    }
}
