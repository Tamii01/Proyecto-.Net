using Data.Base;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Manager
{
	public class RecuperarCuentaManager : BaseManager<Usuarios>
	{
		public override Task<bool> Borrar(Usuarios usuarios)
		{
			throw new NotImplementedException();
		}
		public async override Task<Usuarios> BuscarAsync(LoginDto loginDto)
		{
			return await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Mail == loginDto.Mail);
		}

		public async override Task<List<Usuarios>> BuscarListaAsync()
		{
			return await contextSingleton.Usuarios.Where(x => x.Activo == true).ToListAsync();
		}
	}
}
