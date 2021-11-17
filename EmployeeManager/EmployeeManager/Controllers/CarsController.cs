using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CarsController(ApplicationDbContext db, ICarService carService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carService = carService;
            _userManager = userManager;
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

        [HttpPut("{carId}/assignUser")]
        public async Task<ActionResult> AssignUser([FromBody] string userId, int carId)
        {
            var car = _carService.Get(carId);
            var user = await _userManager.FindByIdAsync(userId);
            car.User = user;
            //_db.SaveChanges();
            return Ok(car);
        }

        [HttpPut("{carId}/dissociateUser")]
        public ActionResult DissociateUser([FromBody] string userId, int carId)
        {
            var car = _carService.Get(carId);

            if (car.User.Id == userId)
            {
                car.User = null;
                _db.SaveChanges();
            }

            return Ok();
        }
    }
}
