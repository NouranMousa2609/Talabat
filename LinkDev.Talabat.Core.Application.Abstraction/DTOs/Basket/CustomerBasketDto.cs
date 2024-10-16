using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Basket
{
	public class CustomerBasketDto
	{
		[Required]
		public required string Id { get; set; }

		public ICollection<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();


	}
}
