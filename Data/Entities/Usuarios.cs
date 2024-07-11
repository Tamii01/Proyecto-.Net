using Data.Dtos;
using Common.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Usuarios
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime Fecha_Nacimiento { get; set; }
		public string Mail { get; set; }
		[ForeignKey("Roles")]
		public int Id_Rol { get; set; }
		public string Clave { get; set; }
		public int? Codigo { get; set; }
		public bool Activo { get; set; }
        public Roles? Roles { get; set; }

        public static implicit operator Usuarios(CrearCuentaDto crearCuentaDto)
		{
			var usuario = new Usuarios();
			usuario.Nombre = crearCuentaDto.Nombre;
			usuario.Apellido = crearCuentaDto.Apellido;
			usuario.Fecha_Nacimiento = crearCuentaDto.FechaNacimiento;
			usuario.Clave = EncryptHelper.Encriptar(crearCuentaDto.Clave);
			usuario.Mail = crearCuentaDto.Mail;
			usuario.Id_Rol = crearCuentaDto.Id_Rol;
			usuario.Activo = crearCuentaDto.Activo;
			return usuario;
		}

		public static implicit operator Usuarios(UsuariosDto recuperarCuentaDto)
		{
			var usuario = new Usuarios();
			usuario.Id = recuperarCuentaDto.Id;
			usuario.Nombre = recuperarCuentaDto.Nombre;
			usuario.Apellido = recuperarCuentaDto.Apellido;
			usuario.Fecha_Nacimiento = recuperarCuentaDto.Fecha_Nacimiento;
			usuario.Clave = recuperarCuentaDto.Clave;
			usuario.Codigo = recuperarCuentaDto.Codigo;
			usuario.Mail = recuperarCuentaDto.Mail;
			usuario.Id_Rol = recuperarCuentaDto.Id_Rol;
			usuario.Activo = recuperarCuentaDto.Activo;
			return usuario;
		}
	}

}
