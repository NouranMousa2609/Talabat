using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey>
		: ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } =  new List<Expression<Func<TEntity, object>>>();


		public BaseSpecifications()
        {
			//Criteria = null;
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = E=>E.Id.Equals(id);
			
		}
    }
}
