using EUROMONITOR.Model;
using EUROMONITOR.Model.DTO;
using EUROMONITOR.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROMONITOR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration) 
        {
            try
            {
                var user = new User
                {
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    UserName = userRegistration.UserName,
                    PasswordHash = userRegistration.Password,
                    Email = userRegistration.Email,
                    PhoneNumber = userRegistration.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, userRegistration.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    { 
                        ModelState.TryAddModelError(error.Code, error.Description);
                    } 
                    return BadRequest(ModelState);
                }

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(400, "One or more validation errors occurred.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                return Unauthorized();
            }

            var userDetail = _authManager.GetAuthenticatedUserDetails();
            HttpContext.Session.SetString(userDetail.Id, user.UserName);

            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
