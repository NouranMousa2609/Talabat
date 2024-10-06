using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.Brands.Any())
			{
				var BrandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (brands?.Count > 0)
				{
					 await dbContext.Set<ProductBrand>().AddRangeAsync(brands);	
					await dbContext.SaveChangesAsync();

				}
			}

			if (!dbContext.Categories.Any())
			{
				var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/Categories.json");
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (Categories?.Count > 0)
				{
					await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
					await dbContext.SaveChangesAsync();

				}
			}
			if (!dbContext.products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					await dbContext.Set<Product>().AddRangeAsync(products);
					await dbContext.SaveChangesAsync();

				}
			}

		}
	}
}
