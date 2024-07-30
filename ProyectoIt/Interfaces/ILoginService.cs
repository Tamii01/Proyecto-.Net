using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProyectoIt.Interfaces
{
    public interface ILoginService
    {
        public  Task<OkObjectResult> ObtenerToken(LoginDto login);
        public Task<OkObjectResult> GuardarUsuario(CrearCuentaDto crearUsuarioDto, string token);
        public  Task<ClaimsPrincipal> ClaimLogin(LoginDto login, string[] resultadoSplit);
        public  Task<Usuarios?> BuscarUsuario(string mail);
        public  Task<bool> CambiarClave(LoginDto loginDto, string mail);
        public  void EnviarMail(LoginDto loginDto);
    }
}
