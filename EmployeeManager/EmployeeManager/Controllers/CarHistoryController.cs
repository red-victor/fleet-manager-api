using EmployeeManager.Data;
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
    [ApiController]
    [Route("api")]
    public class CarHistoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ICarHistoryService _carHistoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarHistoryController(ApplicationDbContext db, ICarHistoryService carHistoryService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carHistoryService = carHistoryService;
            _userManager = userManager;
        }

        [HttpGet("cars/history")]
        public async Task<IEnumerable<CarHistory>> GetAllHistoryAsync()
        {
            return await _carHistoryService.GetAllAsync();
        }

        [HttpGet("cars/{id}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForCarAsync(int id)
        {
            return await _carHistoryService.GetAllForCar(id);
        }

        [HttpGet("history/{id}")]
        public async Task<CarHistory> GetHistoryAsync(int id)
        {
            return await _carHistoryService.GetAsync(id);
        }

        [HttpPost("cars/{id}/history")]
        public async Task<CarHistory> AddCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            return await _carHistoryService.AddAsync(carHistory);
        }

        [HttpPut("cars/{id}/history")]
        public async Task<CarHistory> UpdateCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            return await _carHistoryService.UpdateAsync(carHistory);
        }

        [HttpDelete("cars/{id}/history")]
        public async Task<ActionResult> DeleteCarHistoryAsync(int id)
        {
            await _carHistoryService.RemoveAsync(id);
            return Ok();
        }
    }
}
