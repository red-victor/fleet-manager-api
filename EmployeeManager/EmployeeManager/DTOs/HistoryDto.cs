using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class HistoryDto
    {
        [Required]
        public string AdminId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TicketId { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int ServiceType { get; set; }

        [Required]
        public string Title { get; set; }

        public string Details { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [Required]
        public int MileageAtExecution { get; set; }

        [Required]
        public DateTime RenewDate { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public bool IsPayed { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
