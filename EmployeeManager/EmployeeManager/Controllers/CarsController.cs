using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        private readonly ICarService _carService;
        public CarsController(ApplicationDbContext db, ICarService carService)
        {
            _db = db;
            _carService = carService;
        }

        [HttpPost]
        public JsonResult AddNewCar(Car car)
        {
            _carService.Add(car);
            _db.SaveChanges();
            return Json(new { success = true, responseText = $"Car {car.ChassisSeries} saved" });
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var cars = _carService.GetAll();
            return Ok(cars.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var cars = _carService.Get(id);
            return Ok(cars);
        }
    }
}
