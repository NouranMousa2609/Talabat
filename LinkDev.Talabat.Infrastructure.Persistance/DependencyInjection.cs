using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection Services, IConfiguration configuration)
        {
            #region StoreContext
            Services.AddDbContext<StoreDbContext>((optionsbuilder) =>
            {
                optionsbuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));



            });

            //Services. <IStoreContextInitializer, StoreContextInitializer>();
            Services.AddScoped<IStoreContextInitializer, StoreDbInitializer>();
            Services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreDbInitializer));
            Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            #endregion

            #region IdentityContext
            Services.AddDbContext<StoreIdentityDbContext>((optionsbuilder) =>
            {
                optionsbuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));



            });
            #endregion

            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return Services;
        }

    }
}
