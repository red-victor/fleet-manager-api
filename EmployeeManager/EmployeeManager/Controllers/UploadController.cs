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
            var carList = new List<Car>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                carList = Utils.ParseCarsExcel(stream);
            }

            return Ok(carList);
        }
    }
}
