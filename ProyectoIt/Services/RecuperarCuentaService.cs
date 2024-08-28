using Data.Dtos;
using Data.Entities;
using Data.Manager;
using ProyectoIt.Interfaces;

namespace ProyectoIt.Services
{
	public class RecuperarCuentaService : IRecuperarCuentaService
    {
		private readonly RecuperarCuentaManager _manager;
		private Usuarios _usuario;

        public RecuperarCuentaService()
        {
			_manager = new RecuperarCuentaManager();
            _usuario = new Usuarios();

		}

		public async Task<Usuarios> BuscarUsuarios(LoginDto loginDto)
		{
			return await _manager.BuscarAsync(loginDto);
		}


		public bool GuardarUsuario(UsuariosDto usuarioDto)
		{
            _usuario = usuarioDto;
			return _manager.Guardar(_usuario, _usuario.Id).Result;
		}
    }
}
