using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class TicketDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Details { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
