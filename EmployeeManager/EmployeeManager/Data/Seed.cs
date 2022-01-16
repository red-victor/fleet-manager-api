using EmployeeManager.DTOs;
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
        private static readonly string SEEDPATH = Path.GetFullPath(".\\Data\\Seed").ToString();

        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Cars.Any())
            {
                var carList = new List<Car>();
                var filePath = SEEDPATH + "\\EM_Cars_1000.xlsx";

                var workbook = WorkBook.Load(filePath);
                var worksheet = workbook.GetWorkSheet("data");

                using (context)
                {
                    for (int row = 2; row <= worksheet.RowCount; row++)
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

            if (!context.Users.Any())
            {
                var userList = new List<ApplicationUser>();
                var filePath = SEEDPATH + "\\EM_Users_1000.xlsx";

                var workbook = WorkBook.Load(filePath);
                var worksheet = workbook.GetWorkSheet("data");

                using (context)
                {
                    //var successful = new List<string>();
                    Dictionary<string, string> successful = new Dictionary<string, string>();
                    var failed = new List<string>();

                    for (int row = 2; row <= worksheet.RowCount; row++)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = worksheet[$"A{row}:A{row}"].ToString(),
                            Email = worksheet[$"B{row}:B{row}"].ToString(),
                            FirstName = worksheet[$"C{row}:C{row}"].ToString(),
                            LastName = worksheet[$"D{row}:D{row}"].ToString(),
                            CNP = worksheet[$"E{row}:E{row}"].ToString(),
                            Adress = worksheet[$"F{row}:F{row}"].ToString(),
                            PhoneNumber = worksheet[$"G{row}:G{row}"].ToString(),
                            PhotoUrl = worksheet[$"H{row}:H{row}"].ToString(),
                        };
                        var password = Guid.NewGuid().ToString().Substring(0, 8);
                        var result = await userManager.CreateAsync(user, password);

                        // #todo: Send email to user with generated password

                        if (result.Succeeded)
                        {
                            //successful.Add(user.Email);
                            successful.Add(user.Email, password);
                            await userManager.AddToRoleAsync(user, worksheet[$"I{row}:I{row}"].ToString());
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
