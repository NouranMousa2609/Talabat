using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceService (this IServiceCollection Services,IConfiguration configuration)
		{
			Services.AddDbContext<StoreContext>((optionsbuilder) =>
			{
				optionsbuilder.UseSqlServer(configuration.GetConnectionString("StoreContex"));



			});
			return Services;
		}

	}
}
