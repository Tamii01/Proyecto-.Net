using Data.Base;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class RolesManager : BaseManager<Roles>
    {
        public override Task<bool> Borrar(Roles role)
        {
            throw new NotImplementedException();
        }

        public override Task<Roles> BuscarAsync(LoginDto entity)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Roles>> BuscarListaAsync()
        {
            return await contextSingleton.Roles.Where(x => x.Activo == true).ToListAsync();
        }
    }
}
