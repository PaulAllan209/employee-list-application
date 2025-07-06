using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Infrastructure.Repositories.Interfaces;
using EmployeeListApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeListApplication.Core.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateEmployeeAsnyc(Employee employee)
        {
            await _dbContext.Set<Employee>().AddAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges)
        {
            if (trackChanges)
                return await _dbContext.Set<Employee>().AsTracking().ToListAsync();
            else
            {
                return await _dbContext.Set<Employee>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId, bool trackChanges)
        {
            return await FindByCondition(e => employeeId == e.Id, trackChanges).FirstOrDefaultAsync();
        }

        public void DeleteEmployee(Employee employee)
        {
            _dbContext.Set<Employee>().Remove(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Employee> FindByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
                return _dbContext.Set<Employee>().Where(expression).AsTracking();
            else
                return _dbContext.Set<Employee>().Where(expression).AsNoTracking();
        }
    }
}
