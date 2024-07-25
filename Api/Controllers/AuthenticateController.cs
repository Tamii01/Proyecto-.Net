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

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {

        private static ApplicationDbContext _contextInstance;
        private readonly IConfiguration _configuration;
        public AuthenticateController(IConfiguration configuration)
        {
            _contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validarUsuario = _contextInstance.Usuarios.Where(x => x.Mail == loginDto.Mail && x.Clave == EncryptHelper.Encriptar(loginDto.Password)).Include(x => x.Roles).FirstOrDefault();
            if (validarUsuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre)

                };

                var token = CrearToken(claims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);

            }
            else
            {
                return Unauthorized();
            }
        }

        private JwtSecurityToken CrearToken(List<Claim> claim)
        {
            var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Firma"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(24),
                claims: claim,
                signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
