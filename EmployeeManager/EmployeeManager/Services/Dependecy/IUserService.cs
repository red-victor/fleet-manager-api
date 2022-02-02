using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IUserService : IService<ApplicationUser>
    {
        Task<int> SaveChangesAsync();
        Task<(List<string>, List<string>)> RegisterUsers(List<RegisterDto> userDtos);
        Task<ApplicationUser> ChangeEmail(ApplicationUser userToChange, string newEmail);
        Task<ApplicationUser> TransposeFromDtoAsync(UserDto dto);
        UserDto TransposeToDtoAsync(ApplicationUser user);
        Task<ApplicationUser> GetAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersWithoutCarAsync();
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<PaginationDto<ApplicationUser>> SearchUsers(string str, int page, int pageSize);
        IEnumerable<UserDto> TransposeToDtoAsync(IEnumerable<ApplicationUser> users);
        Task<List<ApplicationUser>> SearchUsersWithNoCar(string str);
        Task<PaginationDto<ApplicationUser>> GetUsersByPageAsync(int page, int pageSize);
        Task<string> SaveProfileImageAsync(IFormFile imgFile);
        void DeleteImage(string imgName);
    }
}
