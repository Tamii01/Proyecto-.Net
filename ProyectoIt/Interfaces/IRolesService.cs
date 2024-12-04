using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Interfaces
{
    public interface IRolesService
    {
        public void GuardarRol(RolesDto rolDto, string token);
        public void EliminarRol(RolesDto rolDto, string token);
    }
}
