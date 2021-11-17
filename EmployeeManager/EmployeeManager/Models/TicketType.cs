using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public enum TicketType
    {
        [Display(Name = "RCA")]
        RCA,
        CASCO,
        ITP,
        Revision,
        Other
    }
}
