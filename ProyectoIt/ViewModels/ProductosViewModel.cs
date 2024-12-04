using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoIt.ViewModels
{
    public class ProductosViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }
        public IFormFile? Imagen_Archivo { get; set; }


        public static implicit operator ProductosViewModel(ProductosDto usuario)
        {
            var usuarioViewModel = new ProductosViewModel();
            usuarioViewModel.Id = usuario.Id;
            usuarioViewModel.Descripcion = usuario.Descripcion;
            usuarioViewModel.Precio = usuario.Precio;
            usuarioViewModel.Stock = usuario.Stock;
            usuarioViewModel.Imagen = usuario.Imagen;
            usuarioViewModel.Activo = usuario.Activo;
            return usuarioViewModel;
        }
    }
}
