using EmployeeManager.DTOs;
using EmployeeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface ICarService : IService<Car>
    {
        Task<IEnumerable<Car>> GetAllAssignedAsync();
        Task<IEnumerable<Car>> GetAllUnassignedAsync();
    }
}
