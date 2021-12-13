using EmployeeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarHistory> CarHistory { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole { Name = "Employee", NormalizedName = "EMPLOYEE" }
            );
        }
    }
}
