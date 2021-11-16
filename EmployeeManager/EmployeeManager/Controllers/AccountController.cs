using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(CredentialDto credentialDto)
        {
            var user = await _userManager.FindByEmailAsync(credentialDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, credentialDto.Password))
            {
                return Unauthorized();
            }

            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CredentialDto credentialDto)
        {
            var user = new ApplicationUser { UserName = credentialDto.Email, Email = credentialDto.Email };
            var result = await _userManager.CreateAsync(user, credentialDto.Password);

            if (!result.Succeeded)
            {
                return ValidationProblem();
            }

            return Ok();
        }
    }
}
