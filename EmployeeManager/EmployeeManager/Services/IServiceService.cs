using EmployeeManager.DTOs;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IServiceService : IService<ServiceToProcess>
    {
        ServiceToProcess TransposeFromDto(ServiceToProcessDto dto);
        ServiceToProcessDto TransposeToDto(ServiceToProcess service);
    }
}
