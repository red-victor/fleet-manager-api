using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class Car
    {
        [Key]
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

        public User User { get; set; }
    }
}
