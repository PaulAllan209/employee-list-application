using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeListApplication.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(User userForRegistration, string password);
        Task<bool> ValidateUser(string username, string password);
        Task<string> CreateToken();
    }
}
