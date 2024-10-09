using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Employees;
using Talabat.Core.Application.Abstraction.Services.Employees;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entites.Employees;
using Talabat.Core.Domain.Specifications.Employees;

namespace Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService(IUnitOfWork unitOfWork,IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
        {
            var spec =new EmployeeWithDepartmentSpecifications();
            
            var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
            
            var employeesToReturn = mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);

            return employeesToReturn;
        }
        public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);

            var employee = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);

            var employeeToReturn = mapper.Map<EmployeeToReturnDto>(employee);

            return employeeToReturn;
        }

    }
}
