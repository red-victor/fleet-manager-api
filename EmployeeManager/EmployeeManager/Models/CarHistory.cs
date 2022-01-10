using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models
{
    public class CarHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public TicketType ServiceType { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Details { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [Required]
        public int MileageAtExecution { get; set; }

        [Required]
        public DateTime RenewDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [Required]
        public bool IsPayed { get; set; }
    }
}
