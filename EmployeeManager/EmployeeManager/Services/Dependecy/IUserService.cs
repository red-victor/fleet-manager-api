using EmployeeManager.DTOs;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IUserService : IService<ApplicationUser>
    {
        ApplicationUser TransposeFromDto(UserDto user);
        UserDto TransposeToDtoAsync(ApplicationUser user);

        IEnumerable<UserDto> TransposeToDtoAsync(IEnumerable<ApplicationUser> users);
    }
}
