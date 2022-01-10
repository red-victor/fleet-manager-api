using EmployeeManager.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Data
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Cars.Any())
            {
                var carList = new List<Car>();

                using (var stream = new MemoryStream())
                {
                    var filePath = Path.GetPathRoot(@"\Seed\");
                    //await file.CopyToAsync(stream);
                    //carList = Utils.ParseCarsExcel(stream);
                }
            }
        }
    }
}
