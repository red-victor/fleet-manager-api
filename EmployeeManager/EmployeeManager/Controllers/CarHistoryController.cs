using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EmployeeManager.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    [ApiController]
    [Route("api/Cars/")]
    public class CarHistoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ICarHistoryService _carHistoryService;
        private readonly ITicketService _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CarHistoryController> _logger;

        public CarHistoryController(ILogger<CarHistoryController> logger, ApplicationDbContext db, ICarHistoryService carHistoryService, ITicketService ticketService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _carHistoryService = carHistoryService;
            _ticketService = ticketService;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("history")]
        public async Task<IEnumerable<CarHistory>> GetAllHistoryAsync()
        {
            var histories = await _carHistoryService.GetAllAsync();
            _logger.LogInformation("All cars histories retrieved");
            return histories;
        }

        [HttpGet("/api/cars/{carId}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForCarAsync(int carId)
        {
            var histories = await _carHistoryService.GetAllForCar(carId);
            _logger.LogInformation($"History for car with id {carId} retrieved");
            return histories;
        }

        [HttpGet("/api/users/{userId}/history")]
        public async Task<IEnumerable<CarHistory>> GetAllForUserAsync(string userId)
        {
            var histories = await _carHistoryService.GetAllForUser(userId);
            _logger.LogInformation($"History for user with id {userId} retrieved");
            return histories;
        }

        [HttpGet("history/{id}")]
        public async Task<CarHistory> GetHistoryAsync(int id)
        {
            var history = await _carHistoryService.GetAsync(id);
            _logger.LogInformation($"History with id {id} retrieved");
            return history;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/history")]
        public async Task<IActionResult> AddCarHistoryAsync(int id, [FromBody] HistoryDto historyDto)
        {
            var ticket = await _ticketService.GetAsync(historyDto.TicketId);
            ticket.Status = (StatusType)historyDto.Status;
            await _ticketService.UpdateAsync(ticket);

            if (ticket.Status == StatusType.Solved)
            {
                var carHistory = new CarHistory()
                {
                    Title = historyDto.Title,
                    Details = historyDto.Details,
                    AdminId = historyDto.AdminId,
                    UserId = historyDto.UserId,
                    TicketId = historyDto.TicketId,
                    ImagePath = historyDto.ImagePath,
                    CarId = historyDto.CarId,
                    MileageAtExecution = historyDto.MileageAtExecution,
                    ExecutionDate = historyDto.ExecutionDate,
                    RenewDate = historyDto.RenewDate,
                    Cost = historyDto.Cost,
                    IsPayed = historyDto.IsPayed,
                    ServiceType = (TicketType)historyDto.ServiceType
                };

                await _carHistoryService.AddAsync(carHistory);
                _logger.LogInformation($"History added for car with id {id}");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Ticket {ticket.Id} status modified, history not added");
                return Ok();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/history")]
        public async Task<CarHistory> UpdateCarHistoryAsync(int id, [FromBody] CarHistory carHistory)
        {
            var history = await _carHistoryService.UpdateAsync(carHistory);
            _logger.LogInformation($"History with id {id} retrieved");
            return history;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/history")]
        public async Task<ActionResult> DeleteCarHistoryAsync(int id)
        {
            await _carHistoryService.RemoveAsync(id);
            _logger.LogInformation($"History with id {id} retrieved");
            return Ok();
        }
    }
}
