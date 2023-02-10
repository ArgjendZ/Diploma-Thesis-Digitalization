using DiplomaThesisDigitalization.Models.DTOs;
using DiplomaThesisDigitalization.Models.Entities;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("add/administrator")]
        public async Task<ActionResult<User>> CreateAdmin([FromQuery] CreateAdminDTO adminDTO)
        {
            await _authenticationService.AddAdmin(adminDTO);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromQuery] LoginDTO login)
        {
            var jwt = await _authenticationService.Login(login);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                message = "Login was successful"
            });
        }

        [HttpGet("logged-user")]
        public async Task<IActionResult> LoggedUser()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt == null)
            {
                return BadRequest("No logged user");
            }
            var loggedUser = await _authenticationService.LoggedUser(jwt);
            if (loggedUser != null)
            {
                return Ok(loggedUser);
            }
            return NotFound();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Logout was successful"
            });
        }
    }
}
