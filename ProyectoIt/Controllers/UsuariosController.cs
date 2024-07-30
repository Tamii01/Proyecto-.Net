using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoIt.Services;
using ProyectoIt.ViewModels;

namespace ProyectoIt.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuariosService;
        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _usuariosService = new UsuariosService(httpClientFactory);
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public IActionResult Usuarios()
        {
            return View();
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuariosDto usuarioDto)
        {
            var usuariosViewModel = await _usuariosService.ListarRolesUsuarios(usuarioDto, HttpContext.Session.GetString("Token"));

            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuariosViewModel);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GuardarUsuario(UsuariosDto usuarioDto)
        {
            _usuariosService.GuardarUsuario(usuarioDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Usuarios", "Usuarios");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarUsuario([FromBody] UsuariosDto usuarioDto)
        {
            usuarioDto.Activo = false;
            _usuariosService.EliminarUsuario(usuarioDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Usuarios", "Usuarios");
        }
    }
}
