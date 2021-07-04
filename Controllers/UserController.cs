using System;
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
        public ActionResult<List<User>> Get() => _user.GetAllUsers();

        [HttpPost("[action]")]
        public ActionResult AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                user.UserId = Convert.ToString(guid);
                user.Password = HashPassword(user.Password);
                _user.AddUser(user);
                return Ok("User successfully added.");
            }

            return BadRequest("Invalid user credentials!");
        }

        [HttpGet("[action]/{key}")]
        public ActionResult<User> GetUser(string key)
        {
            User user = _user.GetUser(key);
            if (user is null)
            {
                return NotFound("User not found!");
            }

            return user;
        }

        [HttpPut("[action]/{key}")]
        public ActionResult UpdateUser(string key, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user credentials!");
            }

            User existingUser = _user.GetUser(key);
            if (existingUser is null)
            {
                return NotFound("User not found!");
            }

            user.Password = HashPassword(user.Password);
            _user.UpdateUser(existingUser, user);
            return Ok("User successfully updated.");
        }

        [HttpDelete("[action]/{key}")]
        public ActionResult DeleteUser(string key)
        {
            User user = _user.GetUser(key);
            if (user is null)
            {
                return NotFound("User not found!");
            }

            _user.DeleteUser(user);
            return Ok("User successfully deleted.");
        }

        [HttpGet("[action]/{key}")]
        public ActionResult<bool> CheckForUser(string key)
        {
            User user = _user.GetUser(key);
            return (user is not null);
        }

        [HttpPost("[action]")]
        public ActionResult Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                user.UserId = Convert.ToString(guid);
                user.Password = HashPassword(user.Password);
                _user.AddUser(user);
                return Ok("User successfully registered.");
            }

            return BadRequest("Invalid user credentials!");
        }

        [HttpPost("[action]")]
        public ActionResult Login([FromBody] UserDto user)
        {
            User requestedUser = _user.GetUser(user.Key);
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
        public ActionResult<User> AuthenticatedUser()
        {
            try
            {
                string jwt = Request.Cookies["jwt"];
                JwtSecurityToken verifiedToken = _jwtService.Verify(jwt);
                string key = verifiedToken.Issuer;
                User user = _user.GetUserById(key);
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
