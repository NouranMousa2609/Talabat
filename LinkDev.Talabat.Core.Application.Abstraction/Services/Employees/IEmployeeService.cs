using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeDto>> GetAllWithSpecAsync();
		Task<EmployeeDto> GetByIdWithSpecAsync(int id);
	}
}
