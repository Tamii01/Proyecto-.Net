using Common.Helpers;
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
            usuario.Clave = _manager.BuscarListaAsync().Result.Where(x => x.Id == usuario.Id).FirstOrDefault().Clave == usuario.Clave ? usuario.Clave : EncryptHelper.Encriptar(usuario.Clave) ;

			return await _manager.Guardar(usuario, crearCuentaDto.Id);
		}

		public async Task<List<Usuarios>> BuscarUsuarios()
		{
			return await _manager.BuscarListaAsync();

        }
	}
}
