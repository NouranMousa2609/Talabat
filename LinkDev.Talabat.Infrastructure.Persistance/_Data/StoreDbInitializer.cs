using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Text.Json;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence.DbInitializers;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreDbInitializer(StoreDbContext _dbContext) :DbInitializer(_dbContext) ,IStoreDbInitializer
	{
    

        //private readonly StoreContext _DbContext;

        //public StoreContextInitializer(StoreContext context)
        //      {
        //	_DbContext = context;
        //}


		public override async Task SeedAsync()
		{
			if (!_dbContext.Brands.Any())
			{
				var BrandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (brands?.Count > 0)
				{
					await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await _dbContext.SaveChangesAsync();

				}
			}

			if (!_dbContext.Categories.Any())
			{
				var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/Categories.json");
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (Categories?.Count > 0)
				{
					await _dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
					await _dbContext.SaveChangesAsync();

				}
			}
			if (!_dbContext.products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					await _dbContext.Set<Product>().AddRangeAsync(products);
					await _dbContext.SaveChangesAsync();

				}
			}
            if (!_dbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/delivary.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (deliveryMethods?.Count > 0)
                {
                    await _dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                    await _dbContext.SaveChangesAsync();

                }
            }
        }
	}
}
