using EmployeeManager.Data;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Add newly stocked Cars To DB
        /// </summary>
        /// <param name="car">Car object</param>
        /// <returns>Success Message</returns>
        [HttpPost]
        public async Task<JsonResult> AddNewCar(CarDto car)
        {
            await _carService.AddAsync(await _carService.TransposeFromDtoAsync(car));
            await _db.SaveChangesAsync();
            return Json(new { success = true, responseText = $"Car {car.ChassisSeries} saved" });
        }

        /// <summary>
        /// Get a List of All Cars with Assigned User
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet("assigned")]
        public async Task<ActionResult> GetAllAssigned()
        {
            var cars = await _carService.GetAllAssignedAsync();
            return Ok(_carService.TransposeToDto(cars));
        }

        /// <summary>
        /// Get a List of All Cars with NO Assigned User
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet("unassigned")]
        public async Task<ActionResult> GetAllUnassigned()
        {
            var cars = await _carService.GetAllUnassignedAsync();
            return Ok(_carService.TransposeToDto(cars));
        }

        /// <summary>
        /// Get a List of All Cars in Db
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(_carService.TransposeToDto(cars));
        }

        /// <summary>
        /// Get ONE Specific Car by corresponding ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>Car Object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var car = await _carService.GetAsync(id);

            if (car != null)
                return Ok(_carService.TransposeToDto(car));
            return NotFound();
        }

        /// <summary>
        /// Delete Car from DB
        /// </summary>
        /// <param name="id">Car Id</param>
        /// <returns>Status Message</returns>
        [HttpDelete]
        public async Task<ActionResult> Remove([FromBody] int id)
        {
            var car = await _carService.GetAsync(id);

            if (car == null)
                return NotFound();

            await _carService.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Assign Car to a User
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="carId">Car Id</param>
        /// <returns>Status Message</returns>
        [HttpPut("{carId}/assignUser")]
        public async Task<ActionResult> AssignUser([FromBody] string userId, int carId)
        {
            var car = await _carService.GetAsync(carId);
            var user = await _userManager.FindByIdAsync(userId);

            if (car.User != null)
                return BadRequest();

            car.User = user;

            if (user.Cars == null)
                user.Cars = new List<Car>();

            user.Cars.Add(car);
            await _db.SaveChangesAsync();
            return Ok(_carService.TransposeToDto(car));
        }

        /// <summary>
        /// Dissociate a Car from a User
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="carId">Car Id</param>
        /// <returns>Status Message</returns>
        [HttpPut("{carId}/dissociateUser")]
        public async Task<ActionResult> DissociateUser([FromBody] string userId, int carId)
        {
            var car = await _carService.GetAsync(carId);
            var user = await _userManager.FindByIdAsync(userId);

            if (car.User != null && car.User.Id == userId)
            {
                car.User = null;
                user.Cars.Remove(car);
                await _db.SaveChangesAsync();
                return Ok(_carService.TransposeToDto(car));
            }

            return BadRequest();
        }
    }
}