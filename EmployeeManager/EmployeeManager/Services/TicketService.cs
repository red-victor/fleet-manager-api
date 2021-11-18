using EmployeeManager.Data;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task AddAsync(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket); 
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Ticket ticket)
        {
            var ticketToUpdate = await GetAsync(ticket.Id);
            ticketToUpdate.Status = ticket.Status;
            ticketToUpdate.Type = ticket.Type;
            await _db.SaveChangesAsync();
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
            return await _db.Tickets.ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var ticket = await GetAsync(id);
            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();
        }

        public async Task<Ticket> TransposeFromDtoAsync(TicketDto dto)
        {
            return new Ticket
            {
                Id = dto.Id,
                User = await _db.Users.FindAsync(dto.UserId),
                Car = await _db.Cars.FindAsync(dto.CarId),
                Title = dto.Title,
                ImagePath = dto.ImagePath,
                Details = dto.Details,
                Date = dto.Date,
                Type = (TicketType)dto.Type,
                Status = dto.Status
            };
        }

        public TicketDto TransposeToDto(Ticket ticket)
        {
            var dto =  new TicketDto
            {
                Id = ticket.Id,
                UserId = ticket.User.Id,
                CarId = ticket.Car.Id,
                Title = ticket.Title,
                ImagePath = ticket.ImagePath,
                Details = ticket.Details,
                Date = ticket.Date,
                Type = (int)ticket.Type,
                Status = ticket.Status
            };

            return dto;
        }
    }
}
