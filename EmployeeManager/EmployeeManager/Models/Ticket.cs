using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [Column("ChassisSeries")]
        public Car Car { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Details { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
