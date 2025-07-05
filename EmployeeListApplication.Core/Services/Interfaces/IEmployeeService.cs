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
        Task UpdateEmployeeAsync();
        Task DeleteEmployeeAsync(string employeeId, bool trackChanges);
    }
}
