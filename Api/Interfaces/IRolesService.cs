using Data.Entities;

namespace Api.Interfaces
{
    public interface IRolesService
    {
        Task<List<Roles>> BuscarRoles();
        Task<bool> GuardarRol(Roles rol);
    }
}
