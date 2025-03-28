﻿using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence.DbInitializers;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection Services, IConfiguration configuration)
        {
            #region StoreContext
            Services.AddDbContext<StoreDbContext>((ServiceProvider, optionsbuilder) =>
            {
                optionsbuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"))
                .AddInterceptors(ServiceProvider.GetRequiredService<AuditInterceptor>());



            });

            Services.AddScoped(typeof(AuditInterceptor));
            //Services. <IStoreContextInitializer, StoreContextInitializer>();
            //Services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();
            Services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));
            //Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));
            #endregion

            #region IdentityContext
            Services.AddDbContext<StoreIdentityDbContext>((optionsbuilder) =>
            {
                optionsbuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));



            });

            Services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

            #endregion

            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return Services;
        }

    }
}
