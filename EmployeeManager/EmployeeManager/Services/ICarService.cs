using EmployeeManager.DTOs;
using EmployeeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface ICarService : IService<Car>
    {
        Task<Car> TransposeFromDtoAsync(CarDto dto);
        Task<List<Car>> TransposeFromDtoAsync(List<CarDto> dtos);
        CarDto TransposeToDto(Car car);
        List<CarDto> TransposeToDto(IEnumerable<Car> cars);
        Task<IEnumerable<Car>> GetAllAssignedAsync();
        Task<IEnumerable<Car>> GetAllUnassignedAsync();
    }
}
