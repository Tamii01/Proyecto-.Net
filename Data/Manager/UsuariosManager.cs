using Data.Base;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Manager
{
	public class UsuariosManager : BaseManager<Usuarios>
	{
		public override Task<bool> Borrar(Usuarios usuarios)
		{
			throw new NotImplementedException();
		}
		public async override Task<Usuarios> BuscarAsync(LoginDto loginDto)
		{
			return await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Activo == true && x.Mail == loginDto.Mail && x.Clave == loginDto.Password);
		}

		public async override Task<List<Usuarios>> BuscarListaAsync()
		{
			return await contextSingleton.Usuarios.Where(x => x.Activo == true).ToListAsync();
		}
	}
}
