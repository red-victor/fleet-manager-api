using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface ICarService : IService<Car>
    {
        Car GetCarByChassis(string chassisSeries);
    }
}
