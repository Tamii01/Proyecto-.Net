using Data.Base;
using Data.Dtos;
using Data.Entities;
using Common.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Data.Manager
{
    public class ProductosManager : BaseManager<Productos>
    {
        public override Task<bool> Borrar(Productos productos)
        {
            throw new NotImplementedException();
        }
        public async override Task<Productos> BuscarAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();

        }


        public async override Task<List<Productos>> BuscarListaAsync()
        {
            return await contextSingleton.Productos.Where(x => x.Activo == true).ToListAsync();
        }
    }
}
