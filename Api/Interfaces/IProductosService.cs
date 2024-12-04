using Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosService
    {
        Task<bool> GuardarProducto(Productos producto);
        Task<List<Productos>> BuscarProductos();
    }
}
