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
            var histories = await _carHistoryService.GetAllAsync();
            _logger.LogInformation("All cars histories retrieved");
            return histories;
        }

        [HttpGet("/api/cars/{carId}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForCarAsync(int id)
        {
            var histories = await _carHistoryService.GetAllForCar(id);
            _logger.LogInformation($"History for car with id {id} retrieved");
            return histories;
        }

        [HttpGet("/api/users/{userId}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForUserAsync(string id)
        {
            var histories = await _carHistoryService.GetAllForUser(id);
            _logger.LogInformation($"History for user with id {id} retrieved");
            return histories;
        }

        [HttpGet("history/{id}")]
        public async Task<CarHistory> GetHistoryAsync(int id)
        {
            var history = await _carHistoryService.GetAsync(id);
            _logger.LogInformation($"History with id {id} retrieved");
            return history;
        }

        [HttpPost("{id}/history")]
        public async Task<CarHistory> AddCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            var history = await _carHistoryService.AddAsync(carHistory);
            _logger.LogInformation($"Car with id {id} history retrieved");
            return history;
        }

        [HttpPut("{id}/history")]
        public async Task<CarHistory> UpdateCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            var history = await _carHistoryService.UpdateAsync(carHistory);
            _logger.LogInformation($"History with id {id} retrieved");
            return history;
        }

        [HttpDelete("{id}/history")]
        public async Task<ActionResult> DeleteCarHistoryAsync(int id)
        {
            await _carHistoryService.RemoveAsync(id);
            _logger.LogInformation($"History with id {id} retrieved");
            return Ok();
        }
    }
}
