using EmployeeManager.Data;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class ServiceController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private readonly ICarService _carService;
        private readonly IServiceService _serviceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceController(ApplicationDbContext db, ICarService carService, IServiceService serviceService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carService = carService;
            _userManager = userManager;
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessService(ServiceToProcessDto dto)
        {
            var service = _serviceService.TransposeFromDto(dto);
            _db.ServicesToProcess.Add(service);
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult> GetService(int serviceId)
        {
            var service = _serviceService.Get(serviceId);
            var dto = _serviceService.TransposeToDto(service);
            return Ok(dto);
        }
    }
}
