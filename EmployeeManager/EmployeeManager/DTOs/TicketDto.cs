using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class TicketDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Details { get; set; }

        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TicketType { get; set; }

        [Required]
        public int MileageAtSubmit { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
