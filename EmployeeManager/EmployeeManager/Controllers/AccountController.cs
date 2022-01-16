using EmployeeManager.DTOs;
using EmployeeManager.Models;
using EmployeeManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager, 
            TokenService tokenService,
            IUserService userService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                if (user != null)
                {
                    _logger.LogInformation($"Failed login the user with email {loginDto.Email}. [ Wrong password ]");
                }
                return Unauthorized();
            }

            var loggedUserDto = new LoggedUserDto 
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.LastName,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                PhotoUrl = user.PhotoUrl,
                Token = await _tokenService.GenerateToken(user)
            };

            _logger.LogInformation($"Successful log in the user with email {loginDto.Email}. Token: {loggedUserDto.Token}");

            return loggedUserDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser 
            { 
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CNP = registerDto.LastName,
                Adress = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                PhotoUrl = registerDto.PhotoUrl
            };

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var result = await _userManager.CreateAsync(user, password);

            // #todo: Send email to user with generated password

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, registerDto.Role);

            _logger.LogInformation($"New account created with email {registerDto.Email}");
            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<LoggedUserDto>> GetCurrentUser()
        {
            var user = await _userService.GetByUsernameAsync(User.Identity.Name);

            return new LoggedUserDto
            {
                Id = user.Id,
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.LastName,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                PhotoUrl = user.PhotoUrl,
                Car = user.Car,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        [HttpPost]
        [Route("/upload/userList")]
        public async Task<IActionResult> UploadUsersExcel(IFormFile file)
        {
            List<RegisterDto> userList;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                userList = Utils.ParseUsersExcel(stream);
                
                var successful = new List<string>();
                var failed = new List<string>();
                foreach (var toRegister in userList)
                {
                    var user = new ApplicationUser
                    {
                        UserName = toRegister.Email,
                        Email = toRegister.Email,
                        FirstName = toRegister.FirstName,
                        LastName = toRegister.LastName,
                        CNP = toRegister.Cnp,
                        Adress = toRegister.Address,
                        PhoneNumber = toRegister.PhoneNumber,
                        PhotoUrl = toRegister.PhotoUrl
                    };
                    var password = Guid.NewGuid().ToString().Substring(0, 8);
                    var result = await _userManager.CreateAsync(user, password);

                    // #todo: Send email to user with generated password

                    if (result.Succeeded)
                    {
                        successful.Add(toRegister.Email);
                        await _userManager.AddToRoleAsync(user, toRegister.Role);
                        _logger.LogInformation($"New account created with email {toRegister.Email}");
                    }
                    else
                    {
                        failed.Add(toRegister.Email);
                        _logger.LogInformation($"Failed to create new account created with email {toRegister.Email}. \n {result.Errors}");
                    }
                }

                return Json(new { SuccessfullyRegistered = successful, FailedToRegister = failed });
            }
        }
    }
}
