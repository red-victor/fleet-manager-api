using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager
{
    public static class Utils
    {
        public static string[] GetTicketTypes()
        {
            var enumLength = Enum.GetNames(typeof(TicketType)).Length;
            string[] values = new string[enumLength];

            for (int i = 0; i < enumLength; i++)
            {
                values[i] = ((TicketType) i).ToString();
            }

            return values;
        }

        public static string[] GetStatusTypes()
        {
            var enumLength = Enum.GetNames(typeof(StatusType)).Length;
            string[] values = new string[enumLength];

            for (int i = 0; i < enumLength; i++)
            {
                values[i] = ((StatusType)i).ToString();
            }

            return values;
        }

        public static List<Car> ParseCarsExcel(MemoryStream stream)
        {
            var list = new List<Car>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    list.Add(new Car
                    {
                        LicencePlate = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        ChassisSeries = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        Brand = worksheet.Cells[row, 3].Value.ToString().Trim(),
                        Model = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        FirstRegistrationDate = DateTime.Parse((worksheet.Cells[row, 5] as ExcelRange).Value.ToString()),
                        Color = worksheet.Cells[row, 6].Value.ToString().Trim(),
                        Mileage = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim())
                    });
                }
            }

            return list;
        }

        public static List<RegisterDto> ParseUsersExcel(MemoryStream stream)
        {
            var list = new List<RegisterDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    list.Add(new RegisterDto
                    {
                        FirstName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        LastName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        Cnp = worksheet.Cells[row, 3].Value.ToString().Trim(),
                        Adress = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        Email = worksheet.Cells[row, 5].Value.ToString(),
                        Password = worksheet.Cells[row, 6].Value.ToString().Trim(),
                        PhoneNumber = worksheet.Cells[row, 7].Value.ToString().Trim(),
                        PhotoUrl = worksheet.Cells[row, 8].Value.ToString().Trim(),
                        Role = worksheet.Cells[row, 9].Value.ToString().Trim()
                    });
                }
            }

            return list;
        }
    }
}
