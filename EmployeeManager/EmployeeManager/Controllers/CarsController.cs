﻿using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EmployeeManager.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Employee,Admin")]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ICarService _carService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ILogger<CarsController> logger, ApplicationDbContext db, ICarService carService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carService = carService;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Car> AddNewCar(CarDto carDto)
        {
            var config = new MapperConfiguration(cfg =>
                       cfg.CreateMap<CarDto, Car>()
                   );
            var mapper = new Mapper(config);
            var car = mapper.Map<Car>(carDto);

            var addedCar = await _carService.AddAsync(car);
            _logger.LogInformation("A new car added. Id: {id}", addedCar.Id);
            return addedCar;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("assigned")]
        public async Task<IEnumerable<Car>> GetAllAssigned()
        {
            var cars = await _carService.GetAllAssignedAsync();
            _logger.LogInformation("All cars with assigned users retrieved");
            return cars;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("unassigned")]
        public async Task<IEnumerable<Car>> GetAllUnassigned()
        {
            var cars = await _carService.GetAllUnassignedAsync();
            _logger.LogInformation("All cars with no assigned users retrieved");
            return cars;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            _logger.LogInformation("All cars retrieved");
            return cars; 
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-by-page")]
        public async Task<ActionResult<PaginationDto<Car>>> GetCarsByPage([FromQuery] int page, [FromQuery] int size)
        {
            return Ok(await _carService.GetCarsByPageAsync(page, size));
        }

        [HttpGet("{id}")]
        public async Task<Car> Get(int id)
        {
            var car = await _carService.GetAsync(id);
            _logger.LogInformation("Car with id {Id} retrieved", id);
            return car;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> Remove([FromBody] int id)
        {
            var car = await _carService.GetAsync(id);

            if (car == null)
                return NotFound(new ProblemDetails { Title = "Car Not Found" });

            await _carService.RemoveAsync(id);
            _logger.LogInformation("Car with id {Id} deleted", id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{carId}/assignUser/{userId}")]
        public async Task<ActionResult> AssignUser([FromRoute] int carId, [FromRoute] string userId)
        {
            var car = await _carService.GetAsync(carId);

            if (car.User != null)
                return BadRequest(new ProblemDetails { Title = "Car already assigned" });

            var user = await _userManager.FindByIdAsync(userId);

            if (user.Car == null)
                user.Car = car;
            else
                return BadRequest(new ProblemDetails { Title = "User already has a Car" });

            car.User = user;
            await _db.SaveChangesAsync();
            _logger.LogInformation("Car with id {IdCar} assigned to user with id {IdUser}", car.Id, user.Id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{carId}/dissociateUser")]
        public async Task<ActionResult> DissociateUser(int carId)
        {
            var car = await _carService.GetAsync(carId);

            if (car.User == null)
                return BadRequest(new ProblemDetails { Title = "No User is Associated with car" });
            
            var user = await _userManager.FindByIdAsync(car.UserId);
            car.User = null;
            car.UserId = null;
            user.Car = null;
            await _db.SaveChangesAsync();
            _logger.LogInformation("Car with id {IdCar} removed from user with id {IdUser}", car.Id, user.Id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<PaginationDto<Car>> SearchCars([FromQuery] string name, [FromQuery] int page, [FromQuery] int pageSize)
        {
            return await _carService.SearchCars(name, page, pageSize);
        }
    }
}