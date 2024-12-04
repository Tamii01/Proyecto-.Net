using Data.Dtos;
using Common.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Productos
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public decimal Precio { get; set; }
		public int Stock { get; set; }
		public string? Imagen { get; set; }
		public bool Activo { get; set; }


        public static implicit operator Productos(ProductosDto productoDto)
		{
			var usuario = new Productos();
			usuario.Id = productoDto.Id;
			usuario.Descripcion = productoDto.Descripcion;
			usuario.Precio = productoDto.Precio;
			usuario.Stock = productoDto.Stock;
			usuario.Imagen = productoDto.Imagen;
			usuario.Activo = productoDto.Activo;
			return usuario;
		}

	}

}
