using Api.Services;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController
    {
        private readonly ProductosService _services;
        public ProductosController()
        {
            _services = new ProductosService();
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public async Task<bool> GuardarProducto(Productos producto)
        {
            return await _services.GuardarProducto(producto);
        }


        [HttpGet]
        [Route("BuscarProducto")]
        public async Task<List<Productos>> BuscarProductos()
        {
            return await _services.BuscarProductos();
        }
    }
}
