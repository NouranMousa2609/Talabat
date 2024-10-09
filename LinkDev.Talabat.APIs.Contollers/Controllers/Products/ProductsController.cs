using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : BaseApiController
	{

		[HttpGet] //GET: Base/api/Products

		public async Task<ActionResult<IEnumerable<ReturnedProductDto>>> GetProducts()
		 {
			var products = await serviceManager.ProductService.GetProductsAsync();

			return Ok(products);
		}

		[HttpGet("{id:int}")] //GET: Base/api/Products
		public async Task<ActionResult<IEnumerable<ReturnedProductDto>>> GetProduct(int id)
		{
			var products = await serviceManager.ProductService.GetProductAsync(id);
			if (products == null) 
				return NotFound(new {StatusCode = 404,message= "not found"});

			return Ok(products);
		}




	}
}
