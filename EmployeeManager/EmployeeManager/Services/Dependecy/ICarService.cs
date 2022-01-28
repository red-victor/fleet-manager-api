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
        Task<List<Car>> AddAsync(List<Car> cars);
        Task<PaginationDto<Car>> SearchCars(string str, int page, int pageSize);
        Task<PaginationDto<Car>> GetCarsByPageAsync(int page, int pageSize);
    }
}
