using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Models;

namespace EmployeeListApplication.Core.Infrastructure.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateEmployeeAsnyc(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId, bool trackChanges);
        void DeleteEmployee(Employee employee);
        Task SaveChangesAsync();
    }
}
