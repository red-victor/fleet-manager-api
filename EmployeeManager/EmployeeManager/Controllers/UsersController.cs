using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using System.IO;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Authorize(Roles = "Employee,Admin")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _userService.TransposeToDtoAsync(users);
            _logger.LogInformation("All users retrieved");
            return userDtos;
        }

        [HttpGet("get-by-page")]
        public async Task<ActionResult<PaginationDto<ApplicationUser>>> GetUsersByPage([FromQuery]int page, [FromQuery]int size)
        {
            return Ok(await _userService.GetUsersByPageAsync(page, size));
        }

        [Authorize(Roles = "Admin")]
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
            userDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            _logger.LogInformation("User with id {Id} retrieved", id);
            return userDto;
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUser([FromForm]UserDto dto)
        {
            var user = await _userService.TransposeFromDtoAsync(dto);
            user.ImgSrc = ((user.ImgName == null || user.ImgName == "") ? null : String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, user.ImgName));
            var updatedUser = await _userService.UpdateAsync(user);
            var updatedUserDto = _userService.TransposeToDtoAsync(updatedUser);
            updatedUserDto.Role = (await _userManager.GetRolesAsync(user))[0];

            _logger.LogInformation($"User with id {updatedUserDto.Id} updated");
            return updatedUserDto;
        }

        [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("download/userList")]
        public async Task<IActionResult> ExportUsers()
        {
            var users = await _userService.GetAllAsync();
            var excel = Utils.ExportUsersExcel(users);
            string excelName = $"CarList-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            using (var memoryStream = new MemoryStream())
            {
                excel.SaveAs(memoryStream);
                var content = memoryStream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<PaginationDto<ApplicationUser>> SearchUsers([FromQuery]string name, [FromQuery] int page, [FromQuery] int pageSize)
        {
            return await _userService.SearchUsers(name, page, pageSize);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search-users-with-no-car/{name}")]
        public async Task<List<ApplicationUser>> SearchUsersWithNoCar(string name)
        {
            return await _userService.SearchUsersWithNoCar(name);
        }
    }
}
