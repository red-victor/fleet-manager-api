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

        [HttpPost]
        public async Task<ActionResult> ProcessTicket(TicketDto dto)
        {
            var ticket = await _ticketService.TransposeFromDtoAsync(dto);
            await _ticketService.AddAsync(ticket);
            return Ok();
        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult> GetTicket(int ticketId)
        {
            var ticket = await _ticketService.GetAsync(ticketId);

            if (ticket == null)
                return NotFound();

            var dto = _ticketService.TransposeToDto(ticket);
            return Ok(dto);
        }
    }
}
