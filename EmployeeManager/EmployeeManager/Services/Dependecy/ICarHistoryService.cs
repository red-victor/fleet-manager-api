using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface ICarHistoryService : IService<CarHistory>
    {
        Task<IEnumerable<CarHistory>> GetAllForCar(int id);
    }
}
