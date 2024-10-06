using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly Lazy<IGenericRepository<Product,int>> _productRepository;
		private readonly Lazy<IGenericRepository<ProductBrand,int>> _BrandsRepository;
		private readonly Lazy<IGenericRepository<ProductCategory,int>> _CategoriesRepository;
		


		public UnitOfWork( StoreContext context)
        {
			_dbContext = context;
			_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
			_BrandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			_CategoriesRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
			//_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext), LazyThreadSafetyMode.ExecutionAndPublication);
			//_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext), true);
			//ProductRepository = new GenericRepository<Product,int>(_dbContext);
			//BrandsRepository=new GenericRepository<ProductBrand,int>(_dbContext);
			//CategoriesRepository=new GenericRepository<ProductCategory,int>(_dbContext);
		}
        public IGenericRepository<Product, int> ProductRepositore =>  _productRepository.Value;
			
		

		public IGenericRepository<ProductBrand, int> BrandsRepositore=> _BrandsRepository.Value;
		

		public IGenericRepository<ProductCategory, int> CategoriesRepositore => _CategoriesRepository.Value;
		

		public Task<int> CompleteAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
