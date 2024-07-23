using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController 
    {
        private readonly ServiciosService _service;
        public ServiciosController()
        {
            _service = new ServiciosService();
        }

        [HttpGet]
        [Route("BuscarServicios")]
        public async Task<List<Servicios>> BuscarServicios()
        {
            return await _service.BuscarServicios();
        }

        [HttpPost]
        [Route("GuardarRol")]
        public async Task<bool> GuardarRol(Servicios servicio)
        {
            return await _service.GuardarRol(servicio);
        }
    }
}
