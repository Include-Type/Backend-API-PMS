using System;
using System.Threading.Tasks;
using IncludeTypeBackend.Services;
using IncludeTypeBackend.Models;
using IncludeTypeBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace IncludeTypeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _user;
        private readonly JwtService _jwtService;

        public UserController(UserService user, JwtService jwtService)
        {
            _user = user;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() => await _user.GetAllUsersAsync();

        [HttpPost("[action]")]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                user.UserId = Convert.ToString(guid);
                user.Password = HashPassword(user.Password);
                await _user.AddUserAsync(user);
                return Ok("User successfully added.");
            }

            return BadRequest("Invalid user credentials!");
        }

        [HttpGet("[action]/{key}")]
        public async Task<ActionResult<User>> GetUser(string key)
        {
            User user = await _user.GetUserAsync(key);
            if (user is null)
            {
                return NotFound("User not found!");
            }

            return user;
        }

        [HttpPut("[action]/{key}")]
        public async Task<ActionResult> UpdateUser(string key, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user credentials!");
            }

            User existingUser = await _user.GetUserAsync(key);
            if (existingUser is null)
            {
                return NotFound("User not found!");
            }

            if (user.Password.Equals(""))
            {
                user.Password = existingUser.Password;
            }
            else
            {
                user.Password = HashPassword(user.Password);
            }

            await _user.UpdateUserAsync(existingUser, user);
            return Ok("User successfully updated.");
        }

        [HttpDelete("[action]/{key}")]
        public async Task<ActionResult> DeleteUser(string key)
        {
            User user = await _user.GetUserAsync(key);
            if (user is null)
            {
                return NotFound("User not found!");
            }

            await _user.DeleteUserAsync(user);
            return Ok("User successfully deleted.");
        }

        [HttpGet("[action]/{key}")]
        public async Task<ActionResult<bool>> CheckForUser(string key)
        {
            User user = await _user.GetUserAsync(key);
            return (user is not null);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                user.UserId = Convert.ToString(guid);
                user.Password = HashPassword(user.Password);
                await _user.AddUserAsync(user);
                return Ok("User successfully registered.");
            }

            return BadRequest("Invalid user credentials!");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login([FromBody] UserDto user)
        {
            User requestedUser = await _user.GetUserAsync(user.Key);
            if (requestedUser is null || !Verify(user.Password, requestedUser.Password))
            {
                return NotFound("Invalid Credentials!");
            }

            string jwt = _jwtService.Generate(requestedUser.UserId);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            return Ok("Login Successfull.");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<User>> AuthenticatedUser()
        {
            try
            {
                string jwt = Request.Cookies["jwt"];
                JwtSecurityToken verifiedToken = _jwtService.Verify(jwt);
                string key = verifiedToken.Issuer;
                User user = await _user.GetUserByIdAsync(key);
                return user;
            }
            catch
            {
                return Unauthorized("Invalid Token");
            }

        }

        [HttpPost("[action]")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            return Ok("Logout Successfull.");
        }
    }
}
