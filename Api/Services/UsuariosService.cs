using Data.Dtos;
using Data.Entities;
using Data.Manager;

namespace Api.Services
{
	public class UsuariosService
	{
		public async Task<bool> GuardarUsuario (CrearCuentaDto crearCuentaDto)
		{
			var usuariosManager = new UsuariosManager();
			var usuario = new Usuarios();
			usuario = crearCuentaDto;
			return await usuariosManager.Guardar(usuario, 0);
		}
	}
}
