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

        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime FirstRegistrationDate { get; set; }

        public string Color { get; set; }

        public int Mileage { get; set; }

        public User User { get; set; }
    }
}
