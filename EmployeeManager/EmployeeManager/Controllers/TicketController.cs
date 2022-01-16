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
            var addedTicket = await _ticketService.AddAsync(ticket);
            _logger.LogInformation($"A new ticket with id {addedTicket.Id} was added");
            return addedTicket;
        }

        [HttpGet("{ticketId}")]
        public async Task<Ticket> GetTicket(int ticketId)
        {
            var ticket = await _ticketService.GetAsync(ticketId);
            _logger.LogInformation($"Ticket with id {ticket.Id} retrieved");
            return ticket;
        }

        [HttpGet]
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllAsync();
            _logger.LogInformation("All tickets retrieved");
            return tickets;
        }

        [HttpGet("/api/cars/{carId}/tickets")]
        public async Task<IEnumerable<Ticket>> GetAllForCar(int carId)
        {
            var tickets = await _ticketService.GetAllForCarAsync(carId);
            _logger.LogInformation($"All tickets for car with id {carId} retrieved");
            return tickets;
        }

        [HttpPut("{id}")]
        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            var updatedTicket = await _ticketService.UpdateAsync(ticket);
            _logger.LogInformation($"Ticket with id {updatedTicket.Id} retrieved");
            return updatedTicket;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTicket([FromBody] int id)
        {
            if (await _ticketService.GetAsync(id) == null)
                return NotFound();

            await _ticketService.RemoveAsync(id);
            _logger.LogInformation($"Ticket with id {id} removed");
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
