using Data.Dtos;
using Data.Entities;

namespace ProyectoIt.Interfaces
{
    public interface IRecuperarCuentaService
    {
        Task<Usuarios> BuscarUsuarios(LoginDto loginDto);
        bool GuardarUsuario(UsuariosDto usuarioDto);
    }
}
