using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
	public class BasketService(IBasketRepository basketRepository,IMapper mapper,IConfiguration configuration) : IBasketService
	{

		public async Task<CustomerBasketDto> GetCustomerBasketAsync(string id)
		{
			var basket =await basketRepository.GetAsync(id);
			if (basket is null)
			     throw new NotFoundException(nameof(CustomerBasket),id);
			return mapper.Map<CustomerBasketDto>(basket);
		}

		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket)
		{
			var mappedbasket = mapper.Map<CustomerBasket>(customerBasket);
			var TimeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));
			var updatedBasket = await basketRepository.UpdateAsync(mappedbasket, TimeToLive);
			if (updatedBasket is null) throw new BadRequestException("A problem with your basket");

			return customerBasket;
		}

		public async Task DeleteCustomerBasketAsync(string id)
		{
			var deleted = await basketRepository.DeleteAsync(id);

			if (!deleted) throw new BadRequestException("Cann't be deleted");
		}

		
	}
}
