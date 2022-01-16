using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _db;

        public TicketService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket); 
            await _db.SaveChangesAsync();
            return ticket;
        }
        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            var ticketToUpdate = await GetAsync(ticket.Id);
            ticketToUpdate.Status = ticket.Status;
            ticketToUpdate.Type = ticket.Type;
            await _db.SaveChangesAsync();

            return ticketToUpdate;
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _db.Tickets.Where(s => s.Id == id)
                .Include(s => s.User)
                .Include(s => s.Car)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _db.Tickets
                .Include(t => t.Car)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var ticket = await GetAsync(id);
            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllForCarAsync(int id)
        {
            return await _db.Tickets
                .Where(t => t.CarId == id)
                .Include(t => t.Car)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllForUserAsync(string id)
        {
            return await _db.Tickets
                .Where(t => t.UserId == id)
                .Include(t => t.Car)
                .Include(t => t.User)
                .ToListAsync();
        }
    }
}
