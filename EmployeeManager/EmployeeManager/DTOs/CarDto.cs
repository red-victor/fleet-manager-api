using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DTOs
{
    public class CarDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ChassisSeries { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DateTime FirstRegistrationDate { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Mileage { get; set; }

        public string UserId { get; set; }
    }
}
