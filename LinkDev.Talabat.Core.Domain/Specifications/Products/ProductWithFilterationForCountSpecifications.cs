using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId,string? Search)
           : base(p =>
			 (string.IsNullOrEmpty(Search) || p.NormalizedName.Contains(Search))
			   &&
			(!brandId.HasValue || p.BrandId == brandId.Value)
              &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )

        {


        }

    }

}
