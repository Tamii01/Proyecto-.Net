using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProyectoIt.Interfaces;

namespace ProyectoIt.Services
{
    public class ServiciosService : IServiciosService
    {

        private readonly BaseApi _baseApi;
        public ServiciosService(IHttpClientFactory httpClientFactory)
        {
            _baseApi = new BaseApi(httpClientFactory);
        }

        public async void GuardarServicio(ServiciosDto rolDto, string token)
        {
            await _baseApi.PostToApi("Servicios/GuardarServicio", rolDto, token);
        }

        public async void EliminarServicio(ServiciosDto rolDto, string token)
        {
            rolDto.Activo = false;
            await _baseApi.PostToApi("Servicios/GuardarServicio", rolDto, token);
        }
    }
}
