using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Manager
{
	public class UsuariosManager : BaseManager<Usuarios>
	{
		public override Task<bool> Borrar(Usuarios usuarios)
		{
			throw new NotImplementedException();
		}
		public override Task<List<Usuarios>> BuscarAsync(Usuarios usuarios)
		{
			throw new NotImplementedException();
		}
		public async override Task<List<Usuarios>> BuscarListaAsync()
		{
			return await contextSingleton.Usuarios.Where(x => x.Activo == true).ToListAsync();
		}
	}
}
