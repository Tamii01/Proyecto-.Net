using Api.Interfaces;
using Data.Entities;
using Data.Manager;

namespace Api.Services
{
    public class ServiciosService : IServiciosService
    {
        private readonly ServiciosManager _manager;

        public ServiciosService()
        {
            _manager = new ServiciosManager();
        }

        public async Task<List<Servicios>> BuscarServicios()
        {
            return await _manager.BuscarListaAsync();
        }

        public async Task<bool> GuardarServicio(Servicios servicio)
        {
            return await _manager.GuardarServicio(servicio);
        }
    }
}
