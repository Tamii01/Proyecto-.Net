using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoIt.ViewModels;
using System.Net.Http;

namespace ProyectoIt.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly BaseApi _baseApi;
        public ServiciosController(IHttpClientFactory httpClientFactory)
        {
            _baseApi = new BaseApi(httpClientFactory);
        }

        public IActionResult Servicios()
        {
            return View();
        }


        public async Task<IActionResult> ServiciosAddPartial([FromBody] ServiciosDto servicioDto)
        {
            var serviciosViewModel = new ServiciosViewModel();
            if (servicioDto != null) {
                serviciosViewModel = servicioDto;
            }
        
            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", serviciosViewModel);
        }

        public async Task<IActionResult> GuardarServicio(ServiciosDto rolDto)
        {
            await _baseApi.PostToApi("Servicios/GuardarServicio", rolDto);
            return RedirectToAction("Servicios", "Servicios");
        }

        public async Task<IActionResult> EliminarServicio([FromBody] ServiciosDto rolDto)
        {
            rolDto.Activo = false;
            await _baseApi.PostToApi("Servicios/GuardarServicio", rolDto);
            return RedirectToAction("Servicios", "Servicios");
        }
    }
}
