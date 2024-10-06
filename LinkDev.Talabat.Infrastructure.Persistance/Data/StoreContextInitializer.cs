using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContextInitializer(StoreContext _DbContext) : IStoreContextInitializer
	{
		//private readonly StoreContext _DbContext;

		//public StoreContextInitializer(StoreContext context)
  //      {
		//	_DbContext = context;
		//}
        public async Task InitializeAsync()
		{
			var PendingMigrations =  _DbContext.Database.GetPendingMigrations();

			if (PendingMigrations.Any())
				await _DbContext.Database.MigrateAsync();
		}

		public async Task SeedAsync()
		{
			if (!_DbContext.Brands.Any())
			{
				var BrandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (brands?.Count > 0)
				{
					await _DbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await _DbContext.SaveChangesAsync();

				}
			}

			if (!_DbContext.Categories.Any())
			{
				var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/Categories.json");
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (Categories?.Count > 0)
				{
					await _DbContext.Set<ProductCategory>().AddRangeAsync(Categories);
					await _DbContext.SaveChangesAsync();

				}
			}
			if (!_DbContext.products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					await _DbContext.Set<Product>().AddRangeAsync(products);
					await _DbContext.SaveChangesAsync();

				}
			}
		}
	}
}
