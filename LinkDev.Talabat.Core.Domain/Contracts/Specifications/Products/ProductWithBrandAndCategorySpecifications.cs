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
		public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId,int PageSize,int PageIndex)
			: base(p =>
			(!brandId.HasValue || p.BrandId == brandId.Value)
			  &&
			(!categoryId.HasValue || p.CategoryId == categoryId.Value)
			)
		{
			AddIncludes();




			switch (sort)
			{

				case "nameDesc":
					AddOrderByDesc(P => P.Name);
					break;

				case "priceAsc":
					//OrderBy = P => P.Price;
					AddOrderBy(P => P.Price);
					break;

				case "priceDesc":
					AddOrderByDesc(P => P.Price);
					break;

				default:
					AddOrderBy(P => P.Name);
					break;

			}

			ApplyPagination(PageSize*(PageIndex-1),PageSize);

		}
		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
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
