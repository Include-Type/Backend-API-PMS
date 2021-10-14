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
using Microsoft.EntityFrameworkCore;

namespace IncludeTypeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _project;

        public ProjectController(ProjectService project) => _project = project;

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProjectTask>>> GetTasks() => await _project.GetAllTasksAsync();

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetTotalTasks() => await _project.GetTotalTasksAsync();
    }
}