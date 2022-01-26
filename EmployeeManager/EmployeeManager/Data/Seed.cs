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
                var filePath = SEEDPATH + "\\EM_Cars_100_1.xlsx";

                var workbook = WorkBook.Load(filePath);
                var worksheet = workbook.GetWorkSheet("data");

                using (context)
                {
                    for (int row = 2; row <= worksheet.RowCount; row++)
                    {
                        carList.Add(new Car
                        {
                            ImagePath = worksheet[$"A{row}:A{row}"].ToString(),
                            LicencePlate = worksheet[$"B{row}:B{row}"].ToString(),
                            ChassisSeries = worksheet[$"C{row}:C{row}"].ToString(),
                            Brand = worksheet[$"D{row}:D{row}"].ToString(),
                            Model = worksheet[$"E{row}:E{row}"].ToString(),
                            FirstRegistrationDate = DateTime.Parse(worksheet[$"F{row}:F{row}"].ToString()),
                            Color = worksheet[$"G{row}:G{row}"].ToString(),
                            Mileage = int.Parse(worksheet[$"H{row}:H{row}"].ToString())
                        });
                    }
                    await context.Cars.AddRangeAsync(carList);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Users.Any())
            {
                await AddTestingAccounts(context, userManager);

                var userList = new List<ApplicationUser>();
                var filePath = SEEDPATH + "\\EM_Users_100_1.xlsx";

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
                            FirstName = worksheet[$"A{row}:A{row}"].ToString(),
                            LastName = worksheet[$"B{row}:B{row}"].ToString(),
                            CNP = worksheet[$"C{row}:C{row}"].ToString(),
                            Adress = worksheet[$"D{row}:D{row}"].ToString(),
                            Email = worksheet[$"E{row}:E{row}"].ToString(),
                            //Password = worksheet[$"F{row}:F{row}"].ToString(),
                            PhoneNumber = worksheet[$"G{row}:G{row}"].ToString(),
                            PhotoUrl = worksheet[$"H{row}:H{row}"].ToString(),
                            UserName = worksheet[$"E{row}:E{row}"].ToString()
                        };
                        var password = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                        var result = await userManager.CreateAsync(user, password);

                        // #todo: Send email to user with generated password

                        if (result.Succeeded)
                        {
                            //successful.Add(user.Email);
                            successful.Add(user.Email, password);
                            await userManager.AddToRoleAsync(user, worksheet[$"I{row}:I{row}"].ToString());
                            await context.SaveChangesAsync();
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

        public static async Task AddTestingAccounts(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                CNP = "1900825479925",
                Adress = "Sunset Boulevard nr. 18",
                Email = "john.doe@testing.com",
                PhoneNumber = "(555) 555-1234",
                PhotoUrl = "https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                UserName = "john.doe@testing.com"
            };

            var employee = new ApplicationUser
            {
                FirstName = "Jane",
                LastName = "Smith",
                CNP = "1961105479925",
                Adress = "Sunrise Avenue nr. 42",
                Email = "jane.smith@testing.com",
                PhoneNumber = "(555) 555-4321",
                PhotoUrl = "https://www.rd.com/wp-content/uploads/2017/09/01-shutterstock_476340928-Irina-Bg.jpg",
                UserName = "john.doe@testing.com"
            };

            var password = "Pa$$w0rd";

            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.CreateAsync(employee, password);
            await userManager.AddToRoleAsync(employee, "Employee");
            await context.SaveChangesAsync();
        }
    }
}
