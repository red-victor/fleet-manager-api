using AutoMapper;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class UsersController : BaseApiController
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
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            return Ok(_userService.GetUsersDto(users));
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User DTO</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound($"User {id} does not exist");

            return Ok(_userService.GetUserDto(user));
        }
    }
}
