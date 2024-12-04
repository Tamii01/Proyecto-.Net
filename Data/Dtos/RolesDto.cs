
namespace Data.Entities
{
	public class RolesDto
    {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public bool Activo { get; set; }

		public static implicit operator RolesDto(Roles rolDto)
		{
			var rol = new RolesDto();
            rol.Id = rolDto.Id;
            rol.Nombre = rolDto.Nombre;
            rol.Activo = rolDto.Activo;
			return rol;
		}
	}

}
