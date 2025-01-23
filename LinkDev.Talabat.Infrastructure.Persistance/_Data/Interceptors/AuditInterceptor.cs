using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
	internal class AuditInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public AuditInterceptor(ILoggedInUserService loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{

			UpdateEntities(eventData.Context);
			return base.SavingChanges(eventData, result);
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext == null)
				return;

			var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
			.Where(e => e.State is EntityState.Added or EntityState.Modified);

			foreach (var entity in entries)
			{
				if (entity.State is EntityState.Added)
				{
					entity.Entity.CreatedBy = _loggedInUserService.UserId!;
					entity.Entity.CreatedOn = DateTime.UtcNow;

				}
				entity.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entity.Entity.LastModifiedOn = DateTime.UtcNow;
			}
		}



		//private void SetPropertyIfExists(object entity, string propertyName, object value)
		//{
		//	var property = entity.GetType().GetProperty(propertyName);
		//	if (property != null && property.CanWrite)
		//	{
		//		property.SetValue(entity, value);
		//	}
		//}

	}	
}