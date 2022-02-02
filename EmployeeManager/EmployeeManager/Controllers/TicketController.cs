using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EmployeeManager.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Authorize(Roles = "Employee,Admin")]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Ticket> ProcessTicket(TicketDto ticketDto)
        {
            var ticketToAdd = new Ticket
            {
                UserId = ticketDto.UserId,
                CarId = ticketDto.CarId,
                Title = ticketDto.Title,
                ImagePath = ticketDto.ImagePath,
                Details = ticketDto.Details,
                Cost = ticketDto.Cost,
                Date = ticketDto.Date,
                MileageAtSubmit = ticketDto.MileageAtSubmit,
                Type = (TicketType) ticketDto.TicketType,
                Status = (StatusType) ticketDto.Status
            };
            var addedTicket = await _ticketService.AddAsync(ticketToAdd);
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

        [Authorize(Roles = "Admin")]
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

        [HttpGet("/api/users/{userId}/tickets")]
        public async Task<IEnumerable<Ticket>> GetAllForUser(string userId)
        {
            var tickets = await _ticketService.GetAllForUserAsync(userId);
            _logger.LogInformation($"All tickets for user with id {userId} retrieved");
            return tickets;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            var updatedTicket = await _ticketService.UpdateAsync(ticket);
            _logger.LogInformation($"Ticket with id {updatedTicket.Id} retrieved");
            return updatedTicket;
        }

        [Authorize(Roles = "Admin")]
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
