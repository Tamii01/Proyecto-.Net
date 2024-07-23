using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoIt.ViewModels
{
    public class ServiciosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ServiciosViewModel(ServiciosDto servicioDto)
        {
            var rolViewModel = new ServiciosViewModel();
            rolViewModel.Id = servicioDto.Id;
            rolViewModel.Nombre = servicioDto.Nombre;
            rolViewModel.Activo = servicioDto.Activo;
            return rolViewModel;
        }
    }
}
