using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class CarService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("ChassisSeries")]
        public Car Car { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Details { get; set; }
        [Required]
        public DateTime ExecutionDate { get; set; }
        [Required]
        public int MileageAtExecution { get; set; }
        [Required]
        public DateTime RenewDate { get; set; }
        [Required]
        public decimal CostInCents { get; set; }
        [Required]
        public bool IsPayed { get; set; }
    }
}
