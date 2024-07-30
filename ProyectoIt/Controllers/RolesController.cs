using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoIt.Services;
using ProyectoIt.ViewModels;
using System.Net.Http;

namespace ProyectoIt.Controllers
{
    public class RolesController : Controller
    {
        private readonly RolesService _rolesService;
        public RolesController(IHttpClientFactory httpClientFactory)
        {
            _rolesService = new RolesService(httpClientFactory);
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public IActionResult Roles()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RolesAddPartial([FromBody] RolesDto rolDto)
        {
            var rolesViewModel = new RolesViewModel();
            if (rolDto != null) {
                rolesViewModel = rolDto;
            }
        
            return PartialView("~/Views/Roles/Partial/RolesAddPartial.cshtml", rolesViewModel);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GuardarRol(RolesDto rolDto)
        {
            _rolesService.GuardarRol(rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Roles", "Roles");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarRol([FromBody] RolesDto rolDto)
        {
            rolDto.Activo = false;
             _rolesService.EliminarRol(rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Roles", "Roles");
        }
    }
}
