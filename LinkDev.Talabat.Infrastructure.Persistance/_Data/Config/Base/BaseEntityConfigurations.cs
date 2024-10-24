using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base
{
	[DbContextType(typeof(StoreDbContext))]
	internal class BaseEntityConfigurations<TEntity , Tkey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			

			builder.Property(E => E.Id)
				.ValueGeneratedOnAdd();

			
		}
	}
}
