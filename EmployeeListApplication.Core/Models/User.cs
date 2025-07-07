using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeeListApplication.Core.Models
{
    public class User : IdentityUser
    {
        // Uses only UserName from IdentityUser class
    }
}
