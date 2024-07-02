using Data.Dtos;
using Data.Entities;
using Data.Manager;

namespace ProyectoIt.Services
{
	public class RecuperarCuentaService
	{
		private readonly RecuperarCuentaManager _manager;

        public RecuperarCuentaService()
        {
			_manager = new RecuperarCuentaManager();

		}

		public async Task<Usuarios> BuscarUsuarios(LoginDto loginDto)
		{
			return await _manager.BuscarAsync(loginDto);
		}


		public bool GuardarUsuario(UsuariosDto usuarioDto)
		{
			var usuario = new Usuarios();
			usuario = usuarioDto;
			return _manager.Guardar(usuario, usuario.Id).Result;
		}
    }
}
