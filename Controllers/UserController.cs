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
                user.Id = Convert.ToString(guid);
                user.Password = HashPassword(user.Password);
                await _user.AddUserAsync(user);
                return Ok("User successfully added.");
            }

            return BadRequest("Invalid user credentials!");
        }

        [HttpGet("[action]/{key}")]
        public async Task<ActionResult<CompleteUserDto>> GetUser(string key)
        {
            try
            {
                return await _user.GetCompleteUserAsync(key);
            }
            catch
            {
                return NotFound("User not found!");
            }
        }

        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<ProfessionalProfile>> GetUserProfessionalProfile(string userId)
        {
            User user = await _user.GetUserByIdAsync(userId);
            if (user is null)
            {
                return NotFound("User not found");
            }

            return await _user.GetUserProfessionalProfileAsync(userId);
        }

        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<Privacy>> GetUserPrivacyProfile(string userId)
        {
            User user = await _user.GetUserByIdAsync(userId);
            if (user is null)
            {
                return NotFound("User not found");
            }

            return await _user.GetUserPrivacyProfileAsync(userId);
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

        [HttpPut("[action]/{key}")]
        public async Task<ActionResult> UpdateUserProfessionalProfile(string key, [FromBody] ProfessionalProfile proProfile)
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

            ProfessionalProfile existingProfile = await _user.GetUserProfessionalProfileAsync(existingUser.Id);
            await _user.UpdateUserProfessionalProfileAsync(existingProfile, proProfile);
            return Ok("User Professional Profile successfully updated.");
        }

        [HttpPut("[action]/{key}")]
        public async Task<ActionResult> UpdateUserPrivacyProfile(string key, [FromBody] Privacy privacyProfile)
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

            Privacy existingPrivacy = await _user.GetUserPrivacyProfileAsync(existingUser.Id);
            await _user.UpdateUserPrivacyProfileAsync(existingPrivacy, privacyProfile);
            return Ok("User Privacy Profile successfully updated.");
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

        [HttpGet("[action]/{keyWithPassword}")]
        public async Task<ActionResult<bool>> CheckPassword(string keyWithPassword)
        {
            string[] temp = keyWithPassword.Split('-');
            User requestedUser = await _user.GetUserAsync(temp[0]);
            if (!Verify(temp[1], requestedUser.Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                user.Id = Convert.ToString(guid);
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

            string jwt = _jwtService.Generate(requestedUser.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok("Login Successfull.");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<CompleteUserDto>> AuthenticatedUser()
        {
            try
            {
                string jwt = Request.Cookies["jwt"];
                JwtSecurityToken verifiedToken = _jwtService.Verify(jwt);
                string userId = verifiedToken.Issuer;
                CompleteUserDto completeUser = await _user.GetCompleteUserAsync(userId);
                return completeUser;
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
