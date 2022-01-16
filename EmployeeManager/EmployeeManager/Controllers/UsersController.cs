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
            var users = await _userService.GetAllAsync();
            var userDtos = _userService.TransposeToDtoAsync(users);
            _logger.LogInformation("All users retrieved");
            return userDtos;
        }

        [HttpGet("with-no-car")]
        public async Task<IEnumerable<UserDto>> GetUsersWithoutCar()
        {
            var users = await _userService.GetAllUsersWithoutCarAsync();
            var userDtos = _userService.TransposeToDtoAsync(users);

            if (users != null) 
                _logger.LogInformation("All users that do not have a car retrieved");

            return userDtos;
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(string id)
        {
            var user = await _userService.GetAsync(id);
            var userDto = _userService.TransposeToDtoAsync(user);
            _logger.LogInformation("User with id {Id} retrieved", id);
            return userDto;
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUser(UserDto dto)
        {
            var user = _userService.TransposeFromDto(dto);
            var updatedUser = await _userService.UpdateAsync(user);
            var updatedUserDto = _userService.TransposeToDtoAsync(updatedUser);

            _logger.LogInformation($"User with id {updatedUserDto.Id} updated");
            return updatedUserDto;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody] int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            await _userService.RemoveAsync(id);

            _logger.LogInformation($"User with id {id} deleted");
            return Ok();
        }
    }
}
