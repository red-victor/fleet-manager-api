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

        [Column("ChassisSeries")]
        public Car Car { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Details { get; set; }
        public DateTime ExecutionDate { get; set; }
        public int MileageAtExecution { get; set; }
        public DateTime RenewDate { get; set; }
        public decimal CostInCents { get; set; }
        public bool IsPayed { get; set; }
    }
}
