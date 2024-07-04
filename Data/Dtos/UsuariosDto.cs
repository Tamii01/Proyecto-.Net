
namespace Data.Entities
{
	public class UsuariosDto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime Fecha_Nacimiento { get; set; }
		public string Mail { get; set; }
		public int Id_Rol { get; set; }
		public string Clave { get; set; }
		public int? Codigo { get; set; }
		public bool Activo { get; set; }

		public static implicit operator UsuariosDto(Usuarios crearCuentaDto)
		{
			var usuario = new UsuariosDto();
			usuario.Id = crearCuentaDto.Id;
			usuario.Nombre = crearCuentaDto.Nombre;
			usuario.Apellido = crearCuentaDto.Apellido;
			usuario.Fecha_Nacimiento = crearCuentaDto.Fecha_Nacimiento;
			usuario.Clave = crearCuentaDto.Clave;
			usuario.Codigo = crearCuentaDto.Codigo;
			usuario.Mail = crearCuentaDto.Mail;
			usuario.Id_Rol = crearCuentaDto.Id_Rol;
			usuario.Activo = crearCuentaDto.Activo;
			return usuario;
		}
	}

}
