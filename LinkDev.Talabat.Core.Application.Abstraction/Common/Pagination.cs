using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public  IEnumerable<T> Data { get; set; }
		
		public Pagination()
        {
            
        }

		public Pagination(int pageSize, int pageIndex ,int count)
		{
			PageSize = pageSize;
			PageIndex = pageIndex;
            Count = count;
		}
	}
}
