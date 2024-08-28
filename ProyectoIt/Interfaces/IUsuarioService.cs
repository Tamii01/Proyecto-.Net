using Data.Dtos;
using Data.Entities;

namespace ProyectoIt.Interfaces
{
    public interface IUsuarioService
    {
        public void GuardarUsuario(UsuariosDto usuarioDto, string token);
        public void EliminarUsuario(UsuariosDto usuarioDto, string token);
        Task<Usuarios> BuscarUsuario(LoginDto loginDto);

    }
}
