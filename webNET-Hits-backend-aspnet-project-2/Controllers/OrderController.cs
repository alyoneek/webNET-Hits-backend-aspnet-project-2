using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Attributes;
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
            var userId = (Guid)HttpContext.Items["UserId"];
            var response = await _orderService.GetConcreteOrder(userId, id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderInfoDto>>> Get()
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            var response = await _orderService.GetListOrders(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = (Guid)HttpContext.Items["UserId"];
            await _orderService.CreateOrder(userId, model);
            return Ok();
        }

        [Authorize]
        [HttpPost("{id:Guid}/status")]
        public async Task<ActionResult> Post([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["UserId"];
            await _orderService.ConfirmOrderDelivery(userId, id);
            return Ok();
        }
    }
}
