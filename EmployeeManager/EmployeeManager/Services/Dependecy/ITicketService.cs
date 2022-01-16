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
        Task<IEnumerable<Ticket>> GetAllForCarAsync(int id);
        Task<IEnumerable<Ticket>> GetAllForUserAsync(string id);
    }
}
