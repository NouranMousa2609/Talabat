using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Employees
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }

		public int? Age { get; set; }

		public decimal Salary { get; set; }

		public int? DepartmentId { get; set; }

		public  string? Department { get; set; }
	}
}
