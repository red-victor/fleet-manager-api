using EmployeeManager.DTOs;
using EmployeeManager.Models;
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

        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Cars.Any())
            {
                var carList = new List<Car>();
                var filePath = SEEDPATH + "\\EM_Cars_100_1.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        carList.Add(new Car
                        {
                            ImagePath = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                            LicencePlate = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            ChassisSeries = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Brand = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            Model = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                            FirstRegistrationDate = DateTime.Parse(worksheet.Cells[row, 6].Value?.ToString()),
                            Color = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                            Mileage = int.Parse(worksheet.Cells[row, 8].Value?.ToString())
                        });
                    }
                }

                await context.Cars.AddRangeAsync(carList);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var userList = new List<ApplicationUser>();
                Dictionary<string, string> successful = new Dictionary<string, string>();
                var failed = new List<string>();

                var filePath = SEEDPATH + "\\EM_Users_100_1.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var user = new ApplicationUser
                        {
                            FirstName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            LastName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            CNP = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Adress = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            Email = worksheet.Cells[row, 5].Value.ToString(),
                            UserName = worksheet.Cells[row, 5].Value.ToString(),
                            PhoneNumber = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            PhotoUrl = worksheet.Cells[row, 8].Value.ToString().Trim()
                        };

                        var password = worksheet.Cells[row, 6].Value.ToString();
                        var result = await userManager.CreateAsync(user, password);

                        if (result.Succeeded)
                        {
                            successful.Add(user.Email, password);
                            await userManager.AddToRoleAsync(user, worksheet.Cells[row, 9].Value.ToString().Trim());
                            userList.Add(user);
                        }
                        else
                        {
                            failed.Add(user.Email);
                        }
                    }
                }
            }
        }
    }
}
