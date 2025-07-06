using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Infrastructure.Interfaces;
using EmployeeListApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeListApplication.Core.Infrastructure
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public DatabaseSeeder(
            ApplicationDbContext dbContext,
            IConfiguration configuration
            )
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            await _dbContext.Database.MigrateAsync();
            await SeedEmployeeDataAsync();
        }

        private async Task SeedEmployeeDataAsync()
        {
            if (!await _dbContext.Employees.AnyAsync())
            {
                var emp_1 = new Employee
                {
                    Id = Guid.Parse("7242F539-229A-4054-B247-E62B5FD17478"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@company.com",
                    PhoneNumber = "987-654-3210",
                    Position = "HR Manager"
                };

                var emp_2 = new Employee
                {
                    Id = Guid.Parse("A3E61343-B0A4-48F9-A5C6-FD6C93C3F60D"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@company.com",
                    PhoneNumber = "123-456-7890",
                    Position = "Software Developer"
                };

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
