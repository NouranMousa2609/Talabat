using LinkDev.Talabat.Core.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications:BaseSpecifications<Order,int>
    {
        public OrderSpecifications(string paymentIntentId)
            :base(order=>order.BuyerEmail==paymentIntentId)
        {
            AddIncludes();
            AddOrderByDesc(order=>order.OrderDate);
            
        }
       
        public OrderSpecifications(string buyerEmail,int orderId)
           : base(order =>order.Id==orderId&& order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
   
        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(order => order.Items);
            Includes.Add(order => order.DeliveryMethod!);

        }
    }
}
