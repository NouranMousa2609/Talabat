using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Basket
{
	public class CustomerBasket : BaseEntity<string>
	{

		public ICollection<BasketItem> Items { get; set; }= new HashSet<BasketItem>();

		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }

		public int? DeliveryMethodId { get; set; }

		public decimal? ShippingPrice { get; set; }
	}
}
