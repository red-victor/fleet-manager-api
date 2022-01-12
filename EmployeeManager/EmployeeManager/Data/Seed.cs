using EmployeeManager.Models;
using IronXL;
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

        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Cars.Any())
            {
                var carList = new List<Car>();
                var filePath = SEEDPATH + "\\EM_Cars_1000.xlsx";

                var workbook = WorkBook.Load(filePath);
                var worksheet = workbook.GetWorkSheet("data");
                var rowCount = worksheet.RowCount;

                using (context)
                {
                    for (int row = 2; row <= rowCount; row++)
                    {
                        carList.Add(new Car
                        {
                            LicencePlate = worksheet[$"A{row}:A{row}"].ToString(), 
                            ChassisSeries = worksheet[$"B{row}:B{row}"].ToString(),
                            Brand = worksheet[$"C{row}:C{row}"].ToString(),
                            Model = worksheet[$"D{row}:D{row}"].ToString(),
                            FirstRegistrationDate = DateTime.Parse(worksheet[$"E{row}:E{row}"].ToString()),
                            Color = worksheet[$"F{row}:F{row}"].ToString(),
                            Mileage = int.Parse(worksheet[$"G{row}:G{row}"].ToString())
                        });
                    }


                    await context.Cars.AddRangeAsync(carList);
                }

            }
        }
    }
}
