using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string ImagePath { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Details { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TicketType Type { get; set; }

        [Required]
        public StatusType Status { get; set; }
    }
}
