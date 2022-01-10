using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UsersController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        /// <summary>
        /// Get a List of ALL Users
        /// </summary>
        /// <returns>List of User DTOs</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return _userService.TransposeToDtoAsync(users);
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User DTO</returns>
        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _userService.TransposeToDtoAsync(user);
        }

        /// <summary>
        /// Update Specific User Properties
        /// </summary>
        /// <param name="dto">User DTO</param>
        /// <returns>Status Message</returns>
        [HttpPut("{id}")]
        public async Task<ApplicationUser> UpdateUser(UserDto dto)
        {
            var user = _userService.TransposeFromDto(dto);
            return await _userService.UpdateAsync(user);
        }

        /// <summary>
        /// Delete User from Db
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Status Message</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody] int id)
        {
            var ticket = await _userService.GetAsync(id);

            if (ticket == null)
                return NotFound();

            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}
