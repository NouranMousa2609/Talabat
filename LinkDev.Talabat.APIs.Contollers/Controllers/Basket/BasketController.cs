using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
	public class BasketController(IServiceManager serviceManager):BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<CustomerBasketDto>> GetBasket (string id)
		{
			var basket =await serviceManager.BasketService.GetCustomerBasketAsync (id);
			return Ok(basket);


		}


		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket (CustomerBasketDto basketDto)
		{
			var basket =await serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);
			return Ok(basket);
		}

		[HttpDelete]
		public async Task DeleteBasket (string id)
		{
			await serviceManager.BasketService.DeleteCustomerBasketAsync(id);
		}
	}
}
