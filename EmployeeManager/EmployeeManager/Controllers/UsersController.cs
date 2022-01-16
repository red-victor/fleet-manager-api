using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
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
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            _logger.LogInformation("All users retrieved");
            var users = await _userService.GetAllAsync();
            return _userService.TransposeToDtoAsync(users);
        }

        [HttpGet("with-no-car")]
        public async Task<IEnumerable<UserDto>> GetUsersWithoutCar()
        {
            var users = await _userService.GetAllUsersWithoutCarAsync();
            if (users != null) _logger.LogInformation("All users that do not have a car retrieved"); 
            return _userService.TransposeToDtoAsync(users);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(string id)
        {
            _logger.LogInformation("User with id {Id} retrieved", id);
            var user = await _userService.GetAsync(id);
            return _userService.TransposeToDtoAsync(user);
        }

        [HttpPut("{id}")]
        public async Task<ApplicationUser> UpdateUser(UserDto dto)
        {
            _logger.LogInformation("User with id {Id} updated", dto.Id);
            var user = _userService.TransposeFromDto(dto);
            return await _userService.UpdateAsync(user);
        }

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
