using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class RequestEmailChangeDto
    {
        public string UserId { get; set; }
        public string newEmail { get; set; }
        public string password { get; set; }
    }
}
