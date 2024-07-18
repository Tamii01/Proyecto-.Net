using Api.Services;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController 
	{
		private readonly UsuariosService _services;
        public UsuariosController()
        {
            _services = new UsuariosService();
        }

        [HttpPost]
		[Route("CrearUsuario")]
		public async Task<bool> CrearUsuario (CrearCuentaDto crearCuentaDto)
		{
			return await _services.GuardarUsuario(crearCuentaDto);
		}


        [HttpGet]
		[Route("BuscarUsuario")]
		public async Task<List<Usuarios>> BuscarUsuarios()
		{
			return await _services.BuscarUsuarios();
		}
	}
}
