using LinkDev.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications.Employees
{
	public class EmployeesWithDepartmentSpacifications:BaseSpecifications<Employee, int>
	{
        public EmployeesWithDepartmentSpacifications():base()
        {
            Includes.Add(E => E.Department!);
        }
        public EmployeesWithDepartmentSpacifications(int id):base(id)
        {
			Includes.Add(E => E.Department!);

		}
	}
}
