using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
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
			=>withTracking?  await _dbContext.Set<TEntity>().ToListAsync(): await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		//{
		//	if (withTracking)
		//	{
		//		return await _dbContext.Set<TEntity>().ToListAsync();
		//	}
		//	return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		//}

		public async Task<TEntity?> GetAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);
		
		public async Task AddAysnc(TEntity entity) =>await _dbContext.AddAsync(entity);

		public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

		public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
	}
}
