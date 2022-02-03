using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class FileController : Controller
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly ILogger<FileController> _logger;

        public FileController(ICarService carService, IUserService userService, ILogger<FileController> logger)
        {
            _carService = carService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("upload/car-excel")]
        public async Task<IActionResult> UploadCarsExcel(IFormFile file)
        {
            var carList = new List<Car>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                carList = Utils.CarStreamToList(stream);
                await _carService.AddAsync(carList);
            }

            _logger.LogInformation("Cars added from uploaded file");
            return Ok(carList);
        }

        [HttpGet]
        [Route("download/car-excel")]
        public async Task<IActionResult> ExportCars()
        {
            var cars = await _carService.GetAllAsync();
            var excel = Utils.ExportCarsExcel(cars);
            string excelName = $"CarList-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            using (var memoryStream = new MemoryStream())
            {
                excel.SaveAs(memoryStream);
                var content = memoryStream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        [HttpPost]
        [Route("upload/user-excel")]
        public async Task<IActionResult> UploadUsersExcel(IFormFile file)
        {
            List<RegisterDto> userList;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                userList = Utils.UsersStreamToList(stream);

                var (successful, failed) = await _userService.RegisterUsers(userList);

                return Json(new { SuccessfullyRegistered = successful, FailedToRegister = failed });
            }
        }

        [HttpGet]
        [Route("download/user-excel")]
        public async Task<IActionResult> ExportUsers()
        {
            var users = await _userService.GetAllAsync();
            var excel = Utils.ExportUsersExcel(users);
            string excelName = $"CarList-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            using (var memoryStream = new MemoryStream())
            {
                excel.SaveAs(memoryStream);
                var content = memoryStream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

    }
}
