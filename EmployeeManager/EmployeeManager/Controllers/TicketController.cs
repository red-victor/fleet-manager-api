using EmployeeManager.Data;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class TicketController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private readonly ITicketService _ticketService;

        public TicketController(ApplicationDbContext db, ITicketService ticketService)
        {
            _db = db;
            _ticketService = ticketService;
        }

        /// <summary>
        /// Submit new Ticket for Admin Processing
        /// </summary>
        /// <param name="dto">Ticket DTO</param>
        /// <returns>Status Message</returns>
        [HttpPost]
        public async Task<ActionResult> ProcessTicket(TicketDto dto)
        {
            var ticket = await _ticketService.TransposeFromDtoAsync(dto);
            await _ticketService.AddAsync(ticket);
            return Ok();
        }

        /// <summary>
        /// Get ONE Specific Ticket by corresponding Id
        /// </summary>
        /// <param name="ticketId">Ticket Id</param>
        /// <returns>Ticket DTO</returns>
        [HttpGet("{ticketId}")]
        public async Task<ActionResult> GetTicket(int ticketId)
        {
            var ticket = await _ticketService.GetAsync(ticketId);

            if (ticket == null)
                return NotFound();

            var dto = _ticketService.TransposeToDto(ticket);
            return Ok(dto);
        }

        /// <summary>
        /// Get a List of All Tickets in Db
        /// </summary>
        /// <returns>List of Ticket DTOs</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllAsync();
            var dto = _ticketService.TransposeToDto(tickets);
            return Ok(dto);
        }

        /// <summary>
        /// Update Specific Ticket Properties
        /// </summary>
        /// <param name="dto">Ticket DTO</param>
        /// <returns>Status Message</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(TicketDto dto)
        {
            var ticket = await _ticketService.TransposeFromDtoAsync(dto);

            if (ticket == null)
                return NotFound();

            await _ticketService.UpdateAsync(ticket);
            return Ok(_ticketService.TransposeToDto(ticket));
        }

        /// <summary>
        /// Delete Ticket from Db
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Status Message</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteTicket([FromBody] int id)
        {
            var ticket = await _ticketService.GetAsync(id);

            if (ticket == null)
                return NotFound();

            await _ticketService.RemoveAsync(id);
                return Ok();
        }
    }
}
