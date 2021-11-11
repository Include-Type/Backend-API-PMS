global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Text;
global using System.IO;
global using System.IdentityModel.Tokens.Jwt;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.OpenApi.Models;
global using Microsoft.IdentityModel.Tokens;

global using static BCrypt.Net.BCrypt;

global using IncludeTypeBackend.Services;
global using IncludeTypeBackend.Models;
global using IncludeTypeBackend.Dtos;