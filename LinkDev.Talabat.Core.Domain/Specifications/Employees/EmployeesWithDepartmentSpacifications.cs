using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Employees
{
    public class EmployeesWithDepartmentSpacifications : BaseSpecifications<Employee, int>
    {
        public EmployeesWithDepartmentSpacifications() : base()
        {
            Includes.Add(E => E.Department!);
        }
        public EmployeesWithDepartmentSpacifications(int id) : base(id)
        {
            Includes.Add(E => E.Department!);

        }
    }
}
