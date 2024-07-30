using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Interfaces
{
    public interface IServiciosService
    {
        public void GuardarServicio(ServiciosDto rolDto, string token);
        public void EliminarServicio(ServiciosDto rolDto, string token);
    }
}
