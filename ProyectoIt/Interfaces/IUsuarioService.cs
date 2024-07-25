using Data.Dtos;
using Data.Entities;

namespace ProyectoIt.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuarios> BuscarUsuario(LoginDto loginDto);
    }
}
