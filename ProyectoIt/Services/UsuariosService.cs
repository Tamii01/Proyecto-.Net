using Data.Dtos;
using Data.Entities;
using Data.Manager;

namespace ProyectoIt.Services
{
    public class UsuariosService
    {
        private readonly UsuariosManager _manager;

        public UsuariosService()
        {
            _manager = new UsuariosManager();
        }

        public async Task<Usuarios> BuscarUsuario(LoginDto loginDto)
        {
            return await _manager.BuscarUsuarioAsync(loginDto);
        }
    }
}
