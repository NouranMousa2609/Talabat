using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications.Products
{
	public class ProductWithFilterationForCountSpecifications :BaseSpecifications<Product,int>
	{
        public ProductWithFilterationForCountSpecifications(int? brandId,int? categoryId)
		   : base(p =>
			(!brandId.HasValue || p.BrandId == brandId.Value)
			  &&
			(!categoryId.HasValue || p.CategoryId == categoryId.Value)
			)

		{
            

        }

    }
	
}
