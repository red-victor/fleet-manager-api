using System.ComponentModel.DataAnnotations;

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
