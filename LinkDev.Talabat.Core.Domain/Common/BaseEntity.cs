using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#nullable disable

namespace LinkDev.Talabat.Core.Domain.Common
{
	public abstract class BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
	{
		public  Tkey Id  { get; set; }

		

	}
}
