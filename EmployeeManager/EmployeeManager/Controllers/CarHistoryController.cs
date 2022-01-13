using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/Cars/")]
    public class CarHistoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ICarHistoryService _carHistoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CarHistoryController> _logger;

        public CarHistoryController(ILogger<CarHistoryController> logger, ApplicationDbContext db, ICarHistoryService carHistoryService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carHistoryService = carHistoryService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("history")]
        public async Task<IEnumerable<CarHistory>> GetAllHistoryAsync()
        {
            _logger.LogInformation("All cars history retrieved");
            return await _carHistoryService.GetAllAsync();
        }

        [HttpGet("{id}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForCarAsync(int id)
        {
            _logger.LogInformation("Car with id {Id} history retrieved", id);
            return await _carHistoryService.GetAllForCar(id);
        }

        [HttpGet("history/{id}")]
        public async Task<CarHistory> GetHistoryAsync(int id)
        {
            _logger.LogInformation("History with id {Id} retrieved", id);
            return await _carHistoryService.GetAsync(id);
        }

        [HttpPost("{id}/history")]
        public async Task<CarHistory> AddCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            _logger.LogInformation("Car with id {Id} history retrieved", id);
            return await _carHistoryService.AddAsync(carHistory);
        }

        [HttpPut("{id}/history")]
        public async Task<CarHistory> UpdateCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            _logger.LogInformation("History with id {Id} retrieved", id);
            return await _carHistoryService.UpdateAsync(carHistory);
        }

        [HttpDelete("{id}/history")]
        public async Task<ActionResult> DeleteCarHistoryAsync(int id)
        {
            _logger.LogInformation("History with id {Id} retrieved", id);
            await _carHistoryService.RemoveAsync(id);
            return Ok();
        }
    }
}
