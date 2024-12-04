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
            return await contextSingleton.Usuarios.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Activo == true && x.Mail == loginDto.Mail && x.Clave == EncryptHelper.Encriptar(loginDto.Password));

        }

        public async Task<Usuarios> BuscarUsuarioAsync(LoginDto loginDto)
        {
            return await contextSingleton.Usuarios.FirstOrDefaultAsync(x => x.Activo == true && x.Mail == loginDto.Mail);
        }

        public async override Task<List<Usuarios>> BuscarListaAsync()
        {

            return await contextSingleton.Usuarios.Where(x => x.Activo == true).Include(x => x.Roles).ToListAsync();
        }
    }
}
