using Data.Base;
using Data.Entities;
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

        public IActionResult Roles()
        {
            return View();
        }


        public async Task<IActionResult> RolesAddPartial([FromBody] RolesDto rolDto)
        {
            var rolesViewModel = new RolesViewModel();
            if (rolDto != null) {
                rolesViewModel = rolDto;
            }
        
            return PartialView("~/Views/Roles/Partial/RolesAddPartial.cshtml", rolesViewModel);
        }

        public async Task<IActionResult> GuardarRol(RolesDto rolDto)
        {
            await _baseApi.PostToApi("Roles/GuardarRol", rolDto);
            return RedirectToAction("Roles", "Roles");
        }

        public async Task<IActionResult> EliminarRol([FromBody] RolesDto rolDto)
        {
            rolDto.Activo = false;
            await _baseApi.PostToApi("Roles/GuardarRol", rolDto);
            return RedirectToAction("Roles", "Roles");
        }
    }
}
