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

namespace EmployeeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginDto">User Credentials</param>
        /// <returns>Session Token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized();
            }

            return new UserTokenDto 
            { 
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="registerDto">User Credentials</param>
        /// <returns>Status Message</returns>
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
                Adress = registerDto.Adress,
                PhoneNumber = registerDto.PhoneNumber,
                PhotoUrl = registerDto.PhotoUrl
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            return StatusCode(201);
        }

        /// <summary>
        /// Get Current User Session Token
        /// </summary>
        /// <returns>Session Token</returns>
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserTokenDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserTokenDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Name = user.FirstName + " " + user.LastName
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
                        CNP = toRegister.CNP,
                        Adress = toRegister.Adress,
                        PhoneNumber = toRegister.PhoneNumber,
                        PhotoUrl = toRegister.PhotoUrl
                    };

                    var result = await _userManager.CreateAsync(user, toRegister.Password);

                    if (result.Succeeded)
                    {
                        successful.Add(toRegister.Email);
                        await _userManager.AddToRoleAsync(user, toRegister.Role);
                    }
                    else
                    {
                        failed.Add(toRegister.Email);
                    }
                }

                return Json(new { SuccessfullyRegistered = successful, FailedToRegister = failed });
            }

            //return Ok(userList);
        }
    }
}
