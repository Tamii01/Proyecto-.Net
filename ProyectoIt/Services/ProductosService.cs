using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Services
{
    public class ProductosService
    {

        private readonly BaseApi _baseAPi;
        private readonly string _token;
        public ProductosService(IHttpClientFactory httpClientFactory)
        {
            _baseAPi = new BaseApi(httpClientFactory);
        }

        public async void GuardarProducto(ProductosDto productosDto, string token)
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
            await _baseAPi.PostToApi("Productos/GuardarProducto", productosDto, token);
        }

        public async void EliminarProducto(ProductosDto productosDto, string token)
        {
            productosDto.Activo = false;
            await _baseAPi.PostToApi("Productos/GuardarProducto", productosDto, token);
         
        }
    }
}
