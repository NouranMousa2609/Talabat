using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Payment
{
    [Authorize]
    public class PaymentController (IPaymentService paymentService) : BaseApiController
    {

        [HttpPost("{basketId}")] //post: // api/payment
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result =await paymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(result);
        }
    }
}
