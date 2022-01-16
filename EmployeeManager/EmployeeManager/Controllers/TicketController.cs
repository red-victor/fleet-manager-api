using EmployeeManager.Data;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger, ApplicationDbContext db, ITicketService ticketService)
        {
            _db = db;
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Ticket> ProcessTicket(Ticket ticket)
        {
            _logger.LogInformation("A new ticket added for car with id {Id}", ticket.CarId);
            return await _ticketService.AddAsync(ticket);
        }

        [HttpGet("{ticketId}")]
        public async Task<Ticket> GetTicket(int ticketId)
        {
            _logger.LogInformation("Ticket with id {Id} retrieved", ticketId);
            return await _ticketService.GetAsync(ticketId);
        }

        [HttpGet]
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            _logger.LogInformation("All tickets retrieved");
            return await _ticketService.GetAllAsync();
        }

        [HttpGet("/api/cars/{carId}/tickets")]
        public async Task<IEnumerable<Ticket>> GetAllForCar(int carId)
        {
            _logger.LogInformation("All tickets for car with id {Id} retrieved", carId);
            return await _ticketService.GetAllForCarAsync(carId);
        }

        [HttpPut("{id}")]
        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            _logger.LogInformation("Ticket with id {Id} retrieved", ticket.Id);
            return await _ticketService.UpdateAsync(ticket);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTicket([FromBody] int id)
        {
            if (await _ticketService.GetAsync(id) == null)
                return NotFound();
            _logger.LogInformation("Ticket with id {Id} removed", id);
            await _ticketService.RemoveAsync(id);
            return Ok();
        }

        [HttpGet("type")]
        public string[] GetTicketTypes()
        {
            return Utils.GetTicketTypes();
        }

        [HttpGet("status")]
        public string[] GetStatusTypes()
        {
            return Utils.GetStatusTypes();
        }
    }
}
