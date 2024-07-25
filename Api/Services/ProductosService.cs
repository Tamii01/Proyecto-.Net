using Api.Interfaces;
using Common.Helpers;
using Data.Dtos;
using Data.Entities;
using Data.Manager;

namespace Api.Services
{
	public class ProductosService : IProductosService
    {
		private readonly ProductosManager _manager;

        public ProductosService()
        {
            _manager = new ProductosManager();
        }
        public async Task<bool> GuardarProducto (Productos producto)
		{
			return await _manager.Guardar(producto, producto.Id);
		}

		public async Task<List<Productos>> BuscarProductos()
		{
			return await _manager.BuscarListaAsync();

        }
	}
}
