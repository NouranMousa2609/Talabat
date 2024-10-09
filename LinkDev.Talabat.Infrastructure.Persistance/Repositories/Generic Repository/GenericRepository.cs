using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
		{
			//if (typeof(TEntity) == typeof(Product))
			//	return withTracking ?
			//		(IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync() 
			//		: (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync();

			 return withTracking? 
			await _dbContext.Set<TEntity>().ToListAsync():
			await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		}

		//{
		//	if (withTracking)
		//	{
		//		return await _dbContext.Set<TEntity>().ToListAsync();
		//	}
		//	return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		//}

		public async Task<TEntity?> GetAsync(TKey id)
		{
			//if (typeof(TEntity) == typeof(Product))
   //              return  _dbContext.Set<Product>().Where(p=>p.Id.Equals (id)).Include(p => p.Brand).Include(p => p.Category).FirstOrDefault() as TEntity;
				


			return await _dbContext.Set<TEntity>().FindAsync(id);
		}
		
		public async Task AddAysnc(TEntity entity) =>await _dbContext.AddAsync(entity);

		public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

		public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool withTracking = false)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();

		}


		public IQueryable <TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
		{
			return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);

		}
	}
}
