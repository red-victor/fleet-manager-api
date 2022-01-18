using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class ResetPasswordMailRequest
    {
        public string ToEmail { get; set; }
        public string Link { get; set; }
    }
}
