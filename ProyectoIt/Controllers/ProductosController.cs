using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIt.Services;
using ProyectoIt.ViewModels;

namespace ProyectoIt.Controllers
{
    public class ProductosController : Controller
    {
        private readonly BaseApi _baseAPi;
        private readonly ProductosService _productosService;
        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _baseAPi = new BaseApi(httpClientFactory);
            _productosService = new ProductosService(httpClientFactory);
        }

        [Authorize(Roles = "Usuarios, Administrador")]
        public IActionResult Productos()
        {
            return View();
        }


        [Authorize(Roles = "Usuarios, Administrador")]
        public IActionResult ProductosAddPartial([FromBody] ProductosDto productoDto)
        {
            var prodViewModel = new ProductosViewModel();
            if(productoDto != null)
            {
                prodViewModel = productoDto;
            }
            return PartialView("~/Views/Productos/Partial/ProductosAddPartial.cshtml", prodViewModel);
        }


        [Authorize(Roles = "Usuarios, Administrador")]
        public async Task<IActionResult> GuardarProducto(ProductosDto productosDto)
        {
            _productosService.GuardarProducto(productosDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Productos", "Productos");
        }


        [Authorize(Roles = "Usuarios, Administrador")]
        public async Task<IActionResult> EliminarProducto([FromBody] ProductosDto productosDto)
        {
            _productosService.EliminarProducto(productosDto, HttpContext.Session.GetString("Token"));
            return RedirectToAction("Productos", "Productos");
        }
    }
}
