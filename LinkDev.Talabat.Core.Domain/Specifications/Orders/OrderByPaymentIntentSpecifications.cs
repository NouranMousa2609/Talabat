using LinkDev.Talabat.Core.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderByPaymentIntentSpecifications:BaseSpecifications<Order,int>
    {
        public OrderByPaymentIntentSpecifications(string PaymentIntentId)
           : base(order => order.PaymentIntentId == PaymentIntentId)
        {


        }
    }
}
