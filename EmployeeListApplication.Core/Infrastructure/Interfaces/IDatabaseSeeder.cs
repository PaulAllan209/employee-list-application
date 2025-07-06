using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeListApplication.Core.Infrastructure.Interfaces
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
