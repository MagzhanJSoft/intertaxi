using IntercityTaxi.Application.DTOs.Order;
using IntercityTaxi.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntercityTaxi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] RequestOrder requestOrder)
        {
            var validator = new RequestOrderValidator();
            var validationResult = validator.Validate(requestOrder);

            if (!validationResult.IsSuccess)
            {
                return BadRequest(new { message = validationResult.Error });
            }

            var userIdClaim = User?.Claims.FirstOrDefault(r => r.Type == "userId")?.Value;

            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return BadRequest(new { message = "Некорректный userId./Invalid userId." });
            }

            var resultCreateOrder = await _orderService.CreateOrderAsync(userId, requestOrder);

            if (!resultCreateOrder.IsSuccess)
            {
                return BadRequest(resultCreateOrder.Error);
            }

            return Ok(resultCreateOrder.Value);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var getOrderResult = await _orderService.GetById(id);

            if (getOrderResult.IsSuccess)
            {
                return Ok(getOrderResult.Value);
            }
            else
            {
                return BadRequest(getOrderResult.Error);
            }

        }

        [HttpGet("cancel/{id}")]
        public async Task<ActionResult> CancelById(Guid id)
        {
            var userIdClaim = User?.Claims.FirstOrDefault(r => r.Type == "userId")?.Value;

            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return BadRequest(new { message = "Некорректный userId./Invalid userId." });
            }

            var cancelOrderResult = await _orderService.CancelById(userId, id);

            if (cancelOrderResult.IsSuccess)
            {
                return Ok(cancelOrderResult.Value);
            }
            else
            {
                return BadRequest(cancelOrderResult.Error);
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var userIdClaim = User?.Claims.FirstOrDefault(r => r.Type == "userId")?.Value;

            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return BadRequest(new { message = "Некорректный userId./Invalid userId." });
            }

            var userRoleClaim = User?.Claims.FirstOrDefault(r => r.Type == "userRole")?.Value;

            if (!Guid.TryParse(userRoleClaim, out Guid userRoleId))
            {
                return BadRequest(new { message = "Некорректный userRole./Invalid userRole." });
            }

            var getOrderResult = await _orderService.Get(userId, userRoleId);

            if (getOrderResult.IsSuccess)
            {
                return Ok(getOrderResult.Value);
            }
            else
            {
                return BadRequest(getOrderResult.Error);
            }

        }
    }
}
