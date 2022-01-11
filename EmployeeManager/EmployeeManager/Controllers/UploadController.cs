using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
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
        private readonly ApplicationDbContext _db;

        private readonly ICarService _carService;

        public UploadController(ApplicationDbContext db, ICarService carService)
        {
            _db = db;
            _carService = carService;
        }

        [Route("/upload/cars/list")]
        public async Task<IActionResult> UploadCarsExcel(IFormFile file)
        {
            var carList = new List<Car>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                carList = Utils.ParseCarsExcel(stream);
                await _carService.AddAsync(carList);
            }

            return Ok(carList);
        }
    }
}
