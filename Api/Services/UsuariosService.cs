using Data.Dtos;
using Data.Entities;
using Data.Manager;

namespace Api.Services
{
	public class UsuariosService
	{
		private readonly UsuariosManager _manager;

        public UsuariosService()
        {
            _manager = new UsuariosManager();
        }
        public async Task<bool> GuardarUsuario (CrearCuentaDto crearCuentaDto)
		{
			var usuario = new Usuarios();
			usuario = crearCuentaDto;
			return await _manager.Guardar(usuario, 0);
		}

		public async Task<List<Usuarios>> BuscarUsuarios()
		{
			return await _manager.BuscarListaAsync();

        }
	}
}
