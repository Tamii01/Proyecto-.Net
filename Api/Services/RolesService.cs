using Data.Entities;
using Data.Manager;

namespace Api.Services
{
    public class RolesService
    {
        private readonly RolesManager _manager;

        public RolesService()
        {
            _manager = new RolesManager();
        }

        public async Task<List<Roles>> BuscarRoles()
        {
            return await _manager.BuscarListaAsync();
        }

        public async Task<bool> GuardarRol(Roles producto)
        {
            return await _manager.Guardar(producto, producto.Id);
        }
    }
}
