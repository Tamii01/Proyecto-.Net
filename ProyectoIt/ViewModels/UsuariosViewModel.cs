using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoIt.ViewModels
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Clave { get; set; }
        public string Mail { get; set; }
        public int Id_Rol { get; set; }
        public bool Activo { get; set; }
        public int Codigo { get; set; }

        public IEnumerable<SelectListItem> Lista_Roles { get; set; }

        public static implicit operator UsuariosViewModel(UsuariosDto usuario)
        {
            var usuarioViewModel = new UsuariosViewModel();
            usuarioViewModel.Id = usuario.Id;
            usuarioViewModel.Nombre = usuario.Nombre;
            usuarioViewModel.Apellido = usuario.Apellido;
            usuarioViewModel.Mail = usuario.Mail;
            usuarioViewModel.Fecha_Nacimiento = usuario.Fecha_Nacimiento;
            usuarioViewModel.Id_Rol = usuario.Id_Rol;
            usuarioViewModel.Clave = usuario.Clave;
            usuarioViewModel.Activo = usuario.Activo;
            return usuarioViewModel;
        }
    }
}
