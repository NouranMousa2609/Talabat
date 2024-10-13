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
        public ProductWithBrandAndCategorySpecifications(string? sort):base()
        {
			AddIncludes();

			AddOrderBy(P => P.Name);


			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort) {

					case "nameDesc":
					AddOrderBy(P => P.Name);
						break;

					case "priceAsc":
						//OrderBy = P => P.Price;
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;

					default:
						break;

				}
			}
		}
        public ProductWithBrandAndCategorySpecifications(int id):base(id)
        {

			AddIncludes();


		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			
				Includes.Add(P => P.Brand!);
				Includes.Add(P => P.Category!);

			
		}
	}
	
}
