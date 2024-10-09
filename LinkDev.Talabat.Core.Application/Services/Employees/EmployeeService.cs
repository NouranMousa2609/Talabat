using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications.Employees;


namespace LinkDev.Talabat.Core.Application.Services.Employees
{
	internal class EmployeeService(IUnitOfWork unitOfWork,IMapper mapper) : IEmployeeService
	{
		public async Task<IEnumerable<EmployeeDto>> GetAllWithSpecAsync()
		{
			var spec =  new EmployeesWithDepartmentSpacifications();
			var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);

			var employeesToReturn =mapper.Map<IEnumerable<EmployeeDto>>(employees);
			return employeesToReturn;
		}

		public async Task<EmployeeDto> GetByIdWithSpecAsync(int id)
		{
			var spec = new EmployeesWithDepartmentSpacifications(id);
			var employees = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);

			var employeeToReturn = mapper.Map<EmployeeDto>(employees);
			return employeeToReturn;
		}
	}
}
