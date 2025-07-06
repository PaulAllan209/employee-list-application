using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Models;

namespace EmployeeListApplication.Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
        Task<Employee> GetEmployeeByIdAsync(string employeeId, bool trackChanges);
        Task UpdateEmployeeAsync(); // This function just calls save changes in the repository layer
        Task DeleteEmployeeAsync(string employeeId, bool trackChanges);
    }
}
