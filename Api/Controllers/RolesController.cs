using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController 
    {
        private readonly RolesService _service;
        public RolesController()
        {
            _service = new RolesService();
        }

        [HttpGet]
        [Route("BuscarRoles")]
        public async Task<List<Roles>> BuscarRoles()
        {
            return await _service.BuscarRoles();
        }

        [HttpPost]
        [Route("GuardarRol")]
        public async Task<bool> GuardarRol(Roles rol)
        {
            return await _service.GuardarRol(rol);
        }
    }
}
