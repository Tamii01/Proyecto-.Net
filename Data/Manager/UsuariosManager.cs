using Data.Base;
using Data.Dtos;
using Data.Entities;
using Common.Helpers;
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
            try
            {
                return await contextSingleton.Usuarios.Include(x=> x.Roles).FirstOrDefaultAsync(x => x.Activo == true && x.Mail == loginDto.Mail && x.Clave == EncryptHelper.Encriptar(loginDto.Password));

            }
            catch (Exception ex)

            {
                GenerateLogHelper.LogError(ex, "UsuariosManager", "BuscarAsync");
                return null;
            }
        }

        public async Task<Usuarios> BuscarUsuarioAsync(LoginDto loginDto)
        {
            try
            {
                return await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Activo == true && x.Mail == loginDto.Mail);
            }
            catch (Exception ex)

            {
                GenerateLogHelper.LogError(ex, "UsuariosManager", "BuscarUsuarioAsync");
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
                GenerateLogHelper.LogError(ex, "UsuariosManager", "BuscarListaAsync");
                return null;
            }

        }
    }
}
