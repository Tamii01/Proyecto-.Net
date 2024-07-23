using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ProyectoIt.ViewModels;

namespace ProyectoIt.Controllers
{
    public class ProductosController : Controller
    {
        private readonly BaseApi _baseAPi;
        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _baseAPi = new BaseApi(httpClientFactory);
        }
        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult ProductosAddPartial([FromBody] ProductosDto productoDto)
        {
            var prodViewModel = new ProductosViewModel();
            if(productoDto != null)
            {
                prodViewModel = productoDto;
            }
            return PartialView("~/Views/Productos/Partial/ProductosAddPartial.cshtml", prodViewModel);
        }

        public async Task<IActionResult> GuardarProducto(ProductosDto productosDto)
        {
            var producto = new Productos();
            if (productosDto.Imagen_Archivo != null)
            {
                using (var ms = new MemoryStream())
                {
                    productosDto.Imagen_Archivo.CopyTo(ms);
                    var imagenBytes = ms.ToArray();
                    productosDto.Imagen = Convert.ToBase64String(imagenBytes);
                }
            }
            producto = productosDto;
            var productos = await _baseAPi.PostToApi("Productos/GuardarProducto", productosDto);
            return RedirectToAction("Productos", "Productos");
        }

        public async Task<IActionResult> EliminarProducto([FromBody] ProductosDto productosDto)
        {
            productosDto.Activo = false;
            var productos = await _baseAPi.PostToApi("Productos/GuardarProducto", productosDto);
            return RedirectToAction("Productos", "Productos");
        }
    }
}
