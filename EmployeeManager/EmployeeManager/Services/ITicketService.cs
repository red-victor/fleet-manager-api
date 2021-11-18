using EmployeeManager.DTOs;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface ITicketService : IService<Ticket>
    {
        Task<Ticket> TransposeFromDtoAsync(TicketDto dto);
        TicketDto TransposeToDto(Ticket ticket);
        IEnumerable<TicketDto> TransposeToDto(IEnumerable<Ticket> tickets);
    }
}
