using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeListApplication.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUser(User userForRegistration, string password)
        {
            var result = await _userManager.CreateAsync(userForRegistration, password);

            return result;
        }
    }
}
