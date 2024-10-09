using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using  LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceService (this IServiceCollection Services,IConfiguration configuration)
		{
			Services.AddDbContext<StoreContext>((optionsbuilder) =>
			{
				optionsbuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));



			});

			//Services. <IStoreContextInitializer, StoreContextInitializer>();
			Services.AddScoped<IStoreContextInitializer,StoreContextInitializer>();
			Services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
			Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
			return Services;
		}

	}
}
