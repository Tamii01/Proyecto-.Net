
using Microsoft.AspNetCore.Http;

namespace Data.Entities
{
	public class ProductosDto
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }
        public IFormFile? Imagen_Archivo { get; set; }

    }

}
