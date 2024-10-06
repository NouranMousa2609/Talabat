using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork:IAsyncDisposable
	{

		public IGenericRepository<Product, int> ProductRepositore { get; }
		public IGenericRepository<ProductBrand, int> BrandsRepositore { get; }
		public IGenericRepository<ProductCategory, int> CategoriesRepositore { get; }

		Task<int> CompleteAsync();

	}
}
