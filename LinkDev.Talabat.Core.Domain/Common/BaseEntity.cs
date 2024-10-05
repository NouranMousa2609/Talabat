using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Common
{
	public abstract class BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
	{
		public required Tkey Id  { get; set; }

		public required string Createdby { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

		public required string LastModifiedBy { get; set; }
		public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;



	}
}
