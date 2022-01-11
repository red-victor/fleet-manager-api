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
        public static string SEEDPATH = Path.GetFullPath(".\\Data\\Seed").ToString();

        public static void SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.Cars.Any())
            {
                var carList = new List<Car>();
                var filePath = SEEDPATH + "EM_Cars_1000.xlsx";

                using (var stream = new MemoryStream())
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        file.CopyTo(stream);
                        Console.WriteLine(file);
                    }
                }
            }
        }
    }
}
