using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager):BaseApiController
    {
        [HttpPost] //POST : /api/Orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail,orderDto);
            return Ok(result);
        }

        [HttpGet] //GET : /api/Orders
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var result =await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(result);

        }
        [HttpGet("{id}")] //GET : /api/Orders/{id}
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrderByIdAsync(buyerEmail, id);
            return Ok(result);

        }

        [HttpGet("DeliveryMethods")] //GET : /api/Orders/DeliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
           
            var result = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(result);

        }
    }
}
