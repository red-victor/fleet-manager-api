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
        UserDto GetUserDto(ApplicationUser user);

        IEnumerable<UserDto> GetUsersDto(IEnumerable<ApplicationUser> users);
    }
}
