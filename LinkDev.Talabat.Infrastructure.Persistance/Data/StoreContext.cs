using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Reflection;
namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContext : DbContext
	{

        public StoreContext( DbContextOptions<StoreContext> Options):base(Options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformations).Assembly);
		}
		public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }

    }
}
