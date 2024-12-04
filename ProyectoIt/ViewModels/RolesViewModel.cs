using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoIt.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(RolesDto rolDto)
        {
            var usuarioViewModel = new RolesViewModel();
            usuarioViewModel.Id = rolDto.Id;
            usuarioViewModel.Nombre = rolDto.Nombre;
            usuarioViewModel.Activo = rolDto.Activo;
            return usuarioViewModel;
        }
    }
}
