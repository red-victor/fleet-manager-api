using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Data
{
    public class Seed
    {
        private static readonly string SEEDPATH = Path.GetFullPath(".\\Data\\Seed").ToString();

        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            if (!context.Cars.Any())
            {
                var carList = new List<Car>();
                var filePath = SEEDPATH + "\\EM_Cars_100_1.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    carList = Utils.ParseCarsExcel(package);
                }

                await context.Cars.AddRangeAsync(carList);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var filePath = SEEDPATH + "\\EM_Users_100_1.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    var userList = Utils.ParseUsersExcel(package);
                    var (successful, failed) = await userService.RegisterUsers(userList);
                }
            }
        }
    }
}
