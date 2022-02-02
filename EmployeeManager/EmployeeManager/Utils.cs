using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        public static List<Car> CarStreamToList(MemoryStream stream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                return ParseCarsExcel(package);
            }
        }

        public static List<Car> ParseCarsExcel(ExcelPackage package)
        {
            var list = new List<Car>();

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                list.Add(new Car
                {
                    ImagePath = worksheet.Cells[row, 1].Value.ToString().Trim(),
                    LicencePlate = worksheet.Cells[row, 2].Value.ToString().Trim(),
                    ChassisSeries = worksheet.Cells[row, 3].Value.ToString().Trim(),
                    Brand = worksheet.Cells[row, 4].Value.ToString().Trim(),
                    Model = worksheet.Cells[row, 5].Value.ToString().Trim(),
                    FirstRegistrationDate = DateTime.Parse((worksheet.Cells[row, 6] as ExcelRange).Value.ToString()),
                    Color = worksheet.Cells[row, 7].Value.ToString().Trim(),
                    Mileage = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim())
                });
            }

            return list;
        }

        public static List<RegisterDto> UsersStreamToList(MemoryStream stream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                return ParseUsersExcel(package);
            }
        }

        public static List<RegisterDto> ParseUsersExcel(ExcelPackage package)
        {
            var list = new List<RegisterDto>();

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                list.Add(new RegisterDto
                {
                    FirstName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                    LastName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                    Cnp = worksheet.Cells[row, 3].Value.ToString().Trim(),
                    Address = worksheet.Cells[row, 4].Value.ToString().Trim(),
                    Email = worksheet.Cells[row, 5].Value.ToString(),
                    Password = worksheet.Cells[row, 6].Value.ToString(),
                    PhoneNumber = worksheet.Cells[row, 7].Value.ToString().Trim(),
                    ImgSrc = worksheet.Cells[row, 8].Value.ToString().Trim(),
                    ImgName = "img",
                    Role = worksheet.Cells[row, 9].Value.ToString().Trim()
                });
            }

            return list;
        }

        public static ExcelPackage ExportCarsExcel(IEnumerable<Car> cars)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Cars");

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "Brand";
            workSheet.Cells[1, 2].Value = "Model";
            workSheet.Cells[1, 3].Value = "Chassis Series";
            workSheet.Cells[1, 4].Value = "License Plate";
            workSheet.Cells[1, 5].Value = "First Registration Date";
            workSheet.Cells[1, 6].Value = "Color";
            workSheet.Cells[1, 7].Value = "Mileage";

            int recordIndex = 2;

            foreach(var car in cars)
            {
                workSheet.Cells[recordIndex, 1].Value = car.Brand;
                workSheet.Cells[recordIndex, 2].Value = car.Model;
                workSheet.Cells[recordIndex, 3].Value = car.ChassisSeries;
                workSheet.Cells[recordIndex, 4].Value = car.LicencePlate;
                workSheet.Cells[recordIndex, 5].Value = car.FirstRegistrationDate.ToString();
                workSheet.Cells[recordIndex, 6].Value = car.Color;
                workSheet.Cells[recordIndex, 7].Value = car.Mileage;

                recordIndex++;
            }

            for (int i = 1; i <= 7; i++)
            {
                workSheet.Column(i).AutoFit();
            }

            return excel;
        }

        public static ExcelPackage ExportUsersExcel(IEnumerable<ApplicationUser> users)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Users");

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "First Name";
            workSheet.Cells[1, 2].Value = "Last Name";
            workSheet.Cells[1, 3].Value = "CNP";
            workSheet.Cells[1, 4].Value = "Address";
            workSheet.Cells[1, 5].Value = "Join Date";
            workSheet.Cells[1, 6].Value = "Email";
            workSheet.Cells[1, 7].Value = "Phone Number";

            int recordIndex = 2;

            foreach (var user in users)
            {
                workSheet.Cells[recordIndex, 1].Value = user.FirstName;
                workSheet.Cells[recordIndex, 2].Value = user.LastName;
                workSheet.Cells[recordIndex, 3].Value = user.CNP;
                workSheet.Cells[recordIndex, 4].Value = user.Adress;
                workSheet.Cells[recordIndex, 5].Value = user.JoinDate.ToString();
                workSheet.Cells[recordIndex, 6].Value = user.Email;
                workSheet.Cells[recordIndex, 7].Value = user.PhoneNumber;

                recordIndex++;
            }

            for (int i = 1; i <= 7; i++)
            {
                workSheet.Column(i).AutoFit();
            }

            return excel;
        }
    }
}
