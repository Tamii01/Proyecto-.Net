using Api.Interfaces;
using Common.Helpers;
using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private static ApplicationDbContext _contextInstance;
        private readonly IConfiguration _configuration;
        public AuthenticateService(IConfiguration configuration)
        {
            _contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> CrearToken(LoginDto loginDto)
        {
            var validarUsuario = await ValidarUsuario(loginDto);
            if (validarUsuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre)

                };

                var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Firma"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(24),
                    claims: claims,
                    signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            return null;
        }

        public async Task<Usuarios> ValidarUsuario(LoginDto loginDto)
        {
            var validarUsuario = await _contextInstance.Usuarios.Where(x => x.Mail == loginDto.Mail && x.Clave == EncryptHelper.Encriptar(loginDto.Password)).Include(x => x.Roles).FirstOrDefaultAsync();
            return validarUsuario;
        }
    }
}

