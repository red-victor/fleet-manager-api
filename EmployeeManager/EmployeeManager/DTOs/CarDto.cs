using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.DTOs
{
    public class CarDto
    {
        public string ImagePath { get; set; }

        [Required]
        public string LicencePlate { get; set; }

        [Required]
        [StringLength(17)]
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
    }
}
