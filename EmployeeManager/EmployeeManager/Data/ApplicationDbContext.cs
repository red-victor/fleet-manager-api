using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarService> CarServices { get; set; }
        public DbSet<ServiceToProcess> ServicesToProcess { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
