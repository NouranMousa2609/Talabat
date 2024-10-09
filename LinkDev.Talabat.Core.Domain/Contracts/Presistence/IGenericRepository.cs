using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Presistence
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey> spec,bool withTracking = false);
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAysnc(TEntity entity);

        void Delete(TEntity entity);
        void Update(TEntity entity);




    }
}
