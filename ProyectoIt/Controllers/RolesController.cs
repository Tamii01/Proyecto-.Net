using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoIt.ViewModels;
using System.Net.Http;

namespace ProyectoIt.Controllers
{
    public class RolesController : Controller
    {
        private readonly BaseApi _baseApi;
        public RolesController(IHttpClientFactory httpClientFactory)
        {
            _baseApi = new BaseApi(httpClientFactory);
        }

        [Authorize(Roles = "Usuarios")]
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
            await _baseApi.PostToApi("Roles/GuardarRol", rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Roles", "Roles");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarRol([FromBody] RolesDto rolDto)
        {
            rolDto.Activo = false;
            await _baseApi.PostToApi("Roles/GuardarRol", rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Roles", "Roles");
        }
    }
}
