using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Reflection;
namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreDbContext : DbContext
	{
		public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }

        public StoreDbContext( DbContextOptions<StoreDbContext> Options):base(Options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformations).Assembly);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
		

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

	}
}
