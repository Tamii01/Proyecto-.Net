using Data.Entities;

namespace Api.Interfaces
{
    public interface IServiciosService
    {
        Task<List<Servicios>> BuscarServicios();
        Task<bool> GuardarServicio(Servicios rol);
    }
}
