using LinkDev.Talabat.Core.Domain.Contracts.Presistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
	{ 
		public static async Task <WebApplication> InitializeStoreContextAsync(this WebApplication app)
		{
			using var Scope = app.Services.CreateScope();
			var service = Scope.ServiceProvider;
			var StoreContextInitializer = service.GetRequiredService<IStoreDbInitializer>();
			var StoreIdentityContextInitializer = service.GetRequiredService<IStoreIdentityDbInitializer>();

			var loggerFactory = service.GetRequiredService<ILoggerFactory>();
			try
			{
				await StoreContextInitializer.InitializAsync();
				await StoreContextInitializer.SeedAsync();

                await StoreIdentityContextInitializer.InitializAsync();
                await StoreIdentityContextInitializer.SeedAsync();



            }
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();

				logger.LogError(ex, "An error has occured");

			}

			return app; ;

		}
	}
}
