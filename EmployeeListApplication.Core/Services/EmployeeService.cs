using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Infrastructure.Repositories.Interfaces;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services.Interfaces;

namespace EmployeeListApplication.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            await _employeeRepository.CreateEmployeeAsnyc(employee);
            await _employeeRepository.SaveChangesAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges)
        {
            var employeeEntities = await _employeeRepository.GetAllEmployeesAsync(trackChanges);

            return employeeEntities;
        }

        public Task UpdateEmployeeAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployeeAsync(string employeeId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
