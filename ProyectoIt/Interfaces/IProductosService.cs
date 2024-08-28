using Data.Entities;

namespace ProyectoIt.Interfaces
{
    public interface IProductosService
    {
        public void GuardarProducto(ProductosDto productosDto, string token);
        public void EliminarProducto(ProductosDto productosDto, string token);
    }
}
