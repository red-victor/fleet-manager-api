using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get a List of ALL Users
        /// </summary>
        /// <returns>List of User DTOs</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            _logger.LogInformation("All users retrieved");
            var users = await _userService.GetAllAsync();
            return _userService.TransposeToDtoAsync(users);
        }

        /// <summary>
        /// Get a List of Users that do not have a car assigned
        /// </summary>
        /// <returns>List of User DTOs</returns>
        [HttpGet("with-no-car")]
        public async Task<IEnumerable<UserDto>> GetUsersWithoutCar()
        {
            var users = await _userService.GetAllUsersWithoutCarAsync();
            if (users != null) _logger.LogInformation("All users that do not have a car retrieved"); 
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
            _logger.LogInformation("User with id {Id} retrieved", id);
            var user = await _userService.GetAsync(id);
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
            _logger.LogInformation("User with id {Id} updated", dto.Id);
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
            _logger.LogInformation("User with id deleted", id);
            var ticket = await _userService.GetAsync(id);
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}
