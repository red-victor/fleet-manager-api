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
using EmployeeManager.Services.Dependecy;
using System.Security.Claims;
using System.Linq;

namespace EmployeeManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, 
            TokenService tokenService, IUserService userService, IMailService mailService )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _userService = userService;
            _mailService = mailService;
        }

        [AllowAnonymous]
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
                Id = user.Id,
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.CNP,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                PhotoUrl = user.PhotoUrl,
                Token = await _tokenService.GenerateToken(user),
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
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
                CNP = registerDto.Cnp,
                Adress = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                PhoneNumberConfirmed = false,
                PhotoUrl = registerDto.PhotoUrl,
                JoinDate = DateTime.Now
            };

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var result = await _userManager.CreateAsync(user, password);

            try
            {
                var mailRequest = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "AVA Fleet Management Password",
                    Body = password
                };

                await _mailService.SendEmailAsync(mailRequest);
            }
            catch (Exception)
            {
                throw;
            }

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
                CNP = user.CNP,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                PhotoUrl = user.PhotoUrl,
                Car = user.Car,
                Token = await _tokenService.GenerateToken(user),
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
        }

        [HttpPost]
        [Route("upload/userList")]
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
                        PhoneNumberConfirmed = false,
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

        [HttpPut("change-my-password")]
        public async Task<ActionResult>ChangePasswordByOwner(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return BadRequest();
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                    
                }
                _logger.LogInformation($"User {user.Id} failed to change his password. Errors: \n {result.Errors}");
                return ValidationProblem();
            }

            _logger.LogInformation($"User {user.Id} changed his password");
            return Ok();
        }

        [HttpPost("request-reset-password")]
        public async Task<ActionResult> ResetPasswordRequest(RequestResetPasswordDto requestResetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(requestResetPasswordDto.Email);

            if (user == null) return Ok(); // no point to letting the user know the email does not exist, it would only leak information, since it is an unauthorized route.

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            try
            {
                var resetMailRequest = new ResetMailRequest
                {
                    ToEmail = user.Email,
                    UserId = user.Id,
                    Token = token
                };

                await _mailService.SendResetPassEmailAsync(resetMailRequest);
            }
            catch (Exception)
            {
                return Ok();
            }
            return Ok();
        }

        [HttpPost("reset-password")]    
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
            if (user == null) return NotFound();

            if (resetPasswordDto.Password != resetPasswordDto.ConfirmPassword) return BadRequest();

            var token = resetPasswordDto.Token.Replace(' ', '+');

            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("error", error.Description);

                }
                return ValidationProblem();
            }
            return Ok();
        }

        [HttpPost("request-email-change")]
        public async Task<ActionResult> RequestEmailChange(RequestEmailChangeDto requestEmailChangeDto)
        {
            var user = await _userManager.FindByIdAsync(requestEmailChangeDto.UserId);

            if (user == null) return BadRequest();
            if (await _userManager.CheckPasswordAsync(user, requestEmailChangeDto.Password) == false) return BadRequest(new ProblemDetails { Title = "Invalid Password" });

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, requestEmailChangeDto.NewEmail);

            try
            {
                var resetMailRequest = new ResetMailRequest
                {
                    ToEmail = requestEmailChangeDto.NewEmail,
                    UserId = user.Id,
                    Token = token
                };

                await _mailService.SendConfirmEmailEmailAsync(resetMailRequest);
                user.UnConfirmedEmail = requestEmailChangeDto.NewEmail;
                await _userManager.UpdateAsync(user);
                await _userService.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new ProblemDetails { Title = "Something went wrong" });
            }

            return Ok();
        }

        [HttpPut("change-email")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailDto changeEmailDto)
        {
            var user = await _userManager.FindByIdAsync(changeEmailDto.UserId);
            if (user == null || user.UnConfirmedEmail == null) return BadRequest();

            var updatedUser = await _userService.ChangeEmail(user, user.UnConfirmedEmail);

            if (updatedUser == null) return BadRequest(new ProblemDetails { Title = "Something went wrong" });
            return Ok(new LoggedUserDto
            {
                UserName = updatedUser.Email,
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                CNP = updatedUser.CNP,
                Adress = updatedUser.Adress,
                PhoneNumber = updatedUser.PhoneNumber,
                PhotoUrl = updatedUser.PhotoUrl,
                Token = await _tokenService.GenerateToken(updatedUser)
            });
        }
    }
}
