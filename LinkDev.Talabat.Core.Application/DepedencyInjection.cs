using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Services;
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
			services.AddScoped(typeof(IProductService),typeof(ProductService));
			return services;
		}
	}
}
