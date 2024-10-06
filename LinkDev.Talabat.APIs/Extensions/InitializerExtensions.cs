using LinkDev.Talabat.Infrastructure.Persistence.Data;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class InitializerExtensions
	{ 
		public static async Task <WebApplication> InitializeStoreContextAsync(this WebApplication app)
		{
			using var Scope = app.Services.CreateScope();
			var service = Scope.ServiceProvider;
			var StoreContextInitializer = service.GetRequiredService<IStoreContextInitializer>();

			var loggerFactory = service.GetRequiredService<ILoggerFactory>();
			try
			{
				await StoreContextInitializer.InitializeAsync();
				await StoreContextInitializer.SeedAsync();



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
