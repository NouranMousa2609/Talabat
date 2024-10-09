using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications.Products
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
            
        }
    }
	
}
