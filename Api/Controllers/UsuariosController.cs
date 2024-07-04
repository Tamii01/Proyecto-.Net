using Api.Services;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController 
	{

		[HttpPost]
		[Route("CrearUsuario")]
		public async Task<bool> CrearUsuario (CrearCuentaDto crearCuentaDto)
		{
			var usuariosService = new UsuariosService();
			return await usuariosService.GuardarUsuario(crearCuentaDto);
		}
	}
}
