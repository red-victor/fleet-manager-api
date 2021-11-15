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

        public User User { get; set; }

        [Column("ChassisSeries")]
        public Car Car { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
}
