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
        Task<int> SaveChangesAsync();
        Task<ApplicationUser> ChangeEmail(ApplicationUser userToChange, string newEmail);
        ApplicationUser TransposeFromDto(UserDto user);
        UserDto TransposeToDtoAsync(ApplicationUser user);
        Task<ApplicationUser> GetAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersWithoutCarAsync();
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<PaginationDto<ApplicationUser>> SearchUsers(string str, int page, int pageSize);
        IEnumerable<UserDto> TransposeToDtoAsync(IEnumerable<ApplicationUser> users);
        Task<List<ApplicationUser>> SearchUsersWithNoCar(string str);
        Task<PaginationDto<ApplicationUser>> GetUsersByPageAsync(int page, int pageSize);
    }
}
