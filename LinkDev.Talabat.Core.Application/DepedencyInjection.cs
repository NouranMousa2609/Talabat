using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application
{
    public static class DepedencyInjection
	{
		public static IServiceCollection AddApplicationService (this IServiceCollection services)
		{
			//services.AddAutoMapper(Mapper=>Mapper.AddProfile(new MappingProfile()));
			services.AddAutoMapper(typeof(MappingProfile)); 

			//services.AddScoped(typeof(IProductService),typeof(ProductService));
			services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));

			//services.AddScoped(typeof(IBasketService),typeof(BasketService));
			//services.AddScoped(typeof(Func<IBasketService>) ,typeof(Func<BasketService>));

			services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
			{
				var mapper=serviceProvider.GetService<IMapper>();
				var configuration=serviceProvider.GetService<IConfiguration>();
				var basketRepository=serviceProvider.GetService<IBasketRepository>();

				return new BasketService(basketRepository, mapper, configuration);
			});
			return services;
		}
	}
}
