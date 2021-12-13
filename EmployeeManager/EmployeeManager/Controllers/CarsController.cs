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
        public async Task<Car> AddNewCar(Car car)
        {
            return await _carService.AddAsync(car);
        }

        /// <summary>
        /// Get a List of All Cars with Assigned User
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet("assigned")]
        public async Task<IEnumerable<Car>> GetAllAssigned()
        {
            return await _carService.GetAllAssignedAsync();
        }

        /// <summary>
        /// Get a List of All Cars with NO Assigned User
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet("unassigned")]
        public async Task<IEnumerable<Car>> GetAllUnassigned()
        {
            return await _carService.GetAllUnassignedAsync();
        }

        /// <summary>
        /// Get a List of All Cars in Db
        /// </summary>
        /// <returns>List of Cars</returns>
        [HttpGet]
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _carService.GetAllAsync();
        }

        /// <summary>
        /// Get ONE Specific Car by corresponding ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>Car Object</returns>
        [HttpGet("{id}")]
        public async Task<Car> Get(int id)
        {
            return await _carService.GetAsync(id);
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

            if (car.User != null)
                return BadRequest("Car already assigned");

            var user = await _userManager.FindByIdAsync(userId);

            if (user.Car == null)
                user.Car = car;
            else
                return BadRequest("User already has a Car");

            car.User = user;

            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Dissociate a Car from a User
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="carId">Car Id</param>
        /// <returns>Status Message</returns>
        [HttpPut("{carId}/dissociateUser")]
        public async Task<ActionResult> DissociateUser(int carId)
        {
            var car = await _carService.GetAsync(carId);

            if (car.User == null)
                return BadRequest();
            
            var user = await _userManager.FindByIdAsync(car.UserId);
            car.User = null;
            car.UserId = null;
            user.Car = null;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}