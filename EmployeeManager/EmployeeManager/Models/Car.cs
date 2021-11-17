using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string LicencePlate { get; set; }

        [Required]
        [Column(TypeName = "varchar(17)")]
        public string ChassisSeries { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Model { get; set; }

        [Required]
        public DateTime FirstRegistrationDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Color { get; set; }

        [Required]
        public int Mileage { get; set; }

        public ApplicationUser User { get; set; }
    }
}
