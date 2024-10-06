using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		///private readonly Lazy<IGenericRepository<Product,int>> _productRepository;
		///private readonly Lazy<IGenericRepository<ProductBrand,int>> _BrandsRepository;
		///private readonly Lazy<IGenericRepository<ProductCategory,int>> _CategoriesRepository;
		
		private readonly ConcurrentDictionary<string, object> _Repositories;

		public UnitOfWork( StoreContext context)
        {
			_dbContext = context;
			_Repositories = new ();


			///_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
			///_BrandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			///_CategoriesRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
			///_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext), LazyThreadSafetyMode.ExecutionAndPublication);
			///_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext), true);
			///ProductRepository = new GenericRepository<Product,int>(_dbContext);
			///BrandsRepository=new GenericRepository<ProductBrand,int>(_dbContext);
			///CategoriesRepository=new GenericRepository<ProductCategory,int>(_dbContext);
		}


		/// public IGenericRepository<Product, int> ProductRepositore =>  _productRepository.Value;
		/// public IGenericRepository<ProductBrand, int> BrandsRepositore=> _BrandsRepository.Value;
		///public IGenericRepository<ProductCategory, int> CategoriesRepositore => _CategoriesRepository.Value;

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			///var	typeName =typeof(TEntity).Name; //Product
			///if (_Repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity, TKey>) _Repositories[typeName];
			///
			///var Repository = new GenericRepository<TEntity, TKey>(_dbContext);
			///_Repositories.Add(typeName, Repository);
			///return Repository;
			///
			return(IGenericRepository<TEntity, TKey>)_Repositories.GetOrAdd(typeof(TEntity).Name,(Key)=> new GenericRepository<TEntity, TKey>(_dbContext));

		}
		public async Task<int> CompleteAsync()=> await _dbContext.SaveChangesAsync();
		public async ValueTask DisposeAsync()=>await _dbContext.DisposeAsync();

		
	}
}
