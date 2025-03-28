﻿using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Talabat.Infrastructure.BasketRepository;
using LinkDev.Talabat.Shared.Models;
using LinkDev.Talabat.Infrastructure.PaymentService;
namespace LinkDev.Talabat.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionString = configuration.GetConnectionString("Redis");
				var ConnectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
				return ConnectionMultiplexerObj;


			});

		    services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository.BasketRepository));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService.PaymentService));

			services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));

            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));


			return services;
		}
	}
}
