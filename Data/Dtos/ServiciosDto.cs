
namespace Data.Entities
{
	public class ServiciosDto
    {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public bool Activo { get; set; }

		public static implicit operator ServiciosDto(Servicios servicioDto)
		{
			var servicio = new ServiciosDto();
            servicio.Id = servicioDto.Id;
            servicio.Nombre = servicioDto.Nombre;
            servicio.Activo = servicioDto.Activo;
			return servicio;
		}
	}

}
