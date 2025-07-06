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

            await _employeeRepository.CreateEmployeeAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges)
        {
            var employeeEntities = await _employeeRepository.GetAllEmployeesAsync(trackChanges);

            return employeeEntities;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string employeeId, bool trackChanges)
        {
            if (string.IsNullOrEmpty(employeeId))
                throw new ArgumentNullException("employeeId ID cannot be empty.", nameof(employeeId));

            var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(Guid.Parse(employeeId), trackChanges);

            if (employeeEntity == null)
                throw new Exception($"Employee with ID: '{employeeId}' could not be found.");

            return employeeEntity;
        }

        public async Task UpdateEmployeeAsync()
        {
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(string employeeId, bool trackChanges)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentNullException("Employee ID cannot be empty.", nameof(employeeId));

            var employeeEntityToDelete = await _employeeRepository.GetEmployeeByIdAsync(Guid.Parse(employeeId), trackChanges);

            if (employeeEntityToDelete == null)
                throw new Exception($"Employee with ID: '{employeeId}' could not be found.");

            // Repository layer actions
            _employeeRepository.DeleteEmployee(employeeEntityToDelete);
            await _employeeRepository.SaveChangesAsync();

        }
    }
}
