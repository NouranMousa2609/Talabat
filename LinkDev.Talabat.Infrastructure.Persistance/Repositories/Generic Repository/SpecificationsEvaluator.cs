using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
	public static class SpecificationsEvaluator
		<TEntity, TKey> where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{

		public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> InputQuery ,ISpecifications<TEntity,TKey> spec)
		{
			var query = InputQuery;

			if (spec.Criteria is not null)
			    query = query.Where(spec.Criteria);


			if(spec.OrderByDesc is not null)
				query=query.OrderByDescending(spec.OrderByDesc);

			else if (spec.OrderBy is not null)
				query=query.OrderBy(spec.OrderBy);

			query=spec.Includes.Aggregate(query,(CurentQuery,includeExpression)=>CurentQuery.Include(includeExpression));


			return query;
			
		}
	}
}
