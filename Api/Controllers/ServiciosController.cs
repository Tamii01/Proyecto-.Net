using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
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
        [Route("GuardarServicio")]
        public async Task<bool> GuardarServicio(Servicios servicio)
        {
            return await _service.GuardarServicio(servicio);
        }
    }
}
