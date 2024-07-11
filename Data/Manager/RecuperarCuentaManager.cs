using Common.Helpers;
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
            try
            {
                return loginDto.Password != null ? await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Codigo == loginDto.Codigo && x.Mail == loginDto.Mail) : await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Mail == loginDto.Mail);

            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "RecuperarCuentaManager", "BuscarAsync");
                return null;
            }

        }

        public async override Task<List<Usuarios>> BuscarListaAsync()
        {
            try
            {
                return await contextSingleton.Usuarios.Where(x => x.Activo == true).ToListAsync();
            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "RecuperarCuentaManager", "BuscarListaAsync");
                return null;
            }

        }
    }
}
