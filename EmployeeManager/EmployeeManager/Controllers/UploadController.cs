using EmployeeManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class UploadController : Controller
    {
        [Route("/upload/cars/list")]
        public async Task<IActionResult> UploadCarsExcel(IFormFile file)
        {
            var list = new List<Car>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

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
                            //FirstRegistrationDate = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            Color = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            //Mileage = int.TryParse(worksheet.Cells[row, 7].Value.ToString().Trim())
                        });
                    }
                }
            }

            return Ok(list);
        }
    }
}
