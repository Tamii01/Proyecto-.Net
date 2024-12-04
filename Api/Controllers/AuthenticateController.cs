using Data;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Api.Services;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticateService _authenticateService;
        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
            _authenticateService = new AuthenticateService(configuration);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authenticateService.CrearToken(loginDto);
            var validarUsuario = await _authenticateService.ValidarUsuario(loginDto);
            if (token != null)
            {
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
