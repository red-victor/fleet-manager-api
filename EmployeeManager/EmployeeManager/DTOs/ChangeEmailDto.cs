using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class ChangeEmailDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
