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
    public class ServiciosController : Controller
    {
        private readonly ServiciosService _serviciosService;
        public ServiciosController(IHttpClientFactory httpClientFactory)
        {
            _serviciosService = new ServiciosService(httpClientFactory);
        }


        [Authorize(Roles = "Usuario, Administrador")]
        public IActionResult Servicios()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ServiciosAddPartial([FromBody] ServiciosDto servicioDto)
        {
            var serviciosViewModel = new ServiciosViewModel();
            if (servicioDto != null) {
                serviciosViewModel = servicioDto;
            }
        
            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", serviciosViewModel);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GuardarServicio(ServiciosDto rolDto)
        {
            _serviciosService.GuardarServicio(rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Servicios", "Servicios");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarServicio([FromBody] ServiciosDto rolDto)
        {
            rolDto.Activo = false;
            _serviciosService.EliminarServicio(rolDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Servicios", "Servicios");
        }
    }
}
