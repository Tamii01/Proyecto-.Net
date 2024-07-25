using Data.Dtos;
using Data.Entities;
using Data.Manager;
using ProyectoIt.Interfaces;

namespace ProyectoIt.Services
{
    public class UsuariosService : IUsuarioService
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
