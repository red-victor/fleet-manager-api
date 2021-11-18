using AutoMapper;
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
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<TicketDto, Ticket>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<Ticket>(dto);
        }

        public TicketDto TransposeToDto(Ticket ticket)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Ticket, TicketDto>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<TicketDto>(ticket);
        }

        public IEnumerable<TicketDto> TransposeToDto(IEnumerable<Ticket> tickets)
        {
            var list = new List<TicketDto>();

            foreach (var ticket in tickets)
                list.Add(TransposeToDto(ticket));

            return list;
        }
    }
}
