using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

		public async Task<ActionResult<Pagination<ReturnedProductDto>>> GetProducts([FromQuery] ProductSpecificationParams specificationParams)
		 {
			var products = await serviceManager.ProductService.GetProductsAsync(specificationParams);

			return Ok(products);
		}

		[HttpGet("{id:int}")] //GET: Base/api/Products
		public async Task<ActionResult<IEnumerable<ReturnedProductDto>>> GetProduct(int id)
		{
			var products = await serviceManager.ProductService.GetProductAsync(id);
			//if (products == null) 
			//	return NotFound(new ApiResponse(404,$"the prodduct with id :{id} is not found "));

			return Ok(products);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>>GetBrands()
		{
			var brands =await serviceManager.ProductService.GetBrandsAsync();
			return Ok(brands);
		}
		[HttpGet("Categories")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{
			var Categories = await serviceManager.ProductService.GetCategoriesAsync();
			return Ok(Categories);
		}




	}
}
