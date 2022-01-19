using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    public enum TicketType
    {
        [Display(Name = "RCA")]
        RCA,
        [Display(Name = "CASCO")]
        CASCO,
        [Display(Name = "ITP")]
        ITP,
        [Display(Name = "Revision")]
        Revision,
        [Display(Name = "Revision")]
        Consumable,
        [Display(Name = "Other")]
        Other
    }
}
