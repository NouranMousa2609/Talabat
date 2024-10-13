using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products
{
    public class ProductSpecificationParams
    {
		public string? sort { get; set; }

		public int? brandId { get; set; }
		
		public int? categoryId { get; set; }

		public int PageIndex { get; set; } = 1;

		private const int MaxPageSize = 10;

		private int pageSize=5;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > MaxPageSize? MaxPageSize :value; }
		}


	}
}
