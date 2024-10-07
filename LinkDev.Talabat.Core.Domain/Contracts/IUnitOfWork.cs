namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey> where TKey:IEquatable<TKey>;

		//public IGenericRepository<Product, int> ProductRepositore { get; }
		//public IGenericRepository<ProductBrand, int> BrandsRepositore { get; }
		//public IGenericRepository<ProductCategory, int> CategoriesRepositore { get; }

		Task<int> CompleteAsync();

	}
}
