using Data.Base;
using Data.Dtos;
using Data.Entities;
using Data.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoIt.Interfaces;
using ProyectoIt.ViewModels;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ProyectoIt.Services
{
    public class UsuariosService : IUsuarioService
    {
        private readonly UsuariosManager _manager;
        private readonly BaseApi _baseApi;

        public UsuariosService(IHttpClientFactory httpClientFactory)
        {
            _manager = new UsuariosManager();
            _baseApi = new BaseApi(httpClientFactory);
        }

        public async void GuardarUsuario(UsuariosDto usuarioDto, string token)
        {
            await _baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto, token);
        }

        public async void EliminarUsuario(UsuariosDto usuarioDto, string token)
        {
            usuarioDto.Activo = false;
            await _baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto, token);
        }

        public async Task<UsuariosViewModel> ListarRolesUsuarios([FromBody] UsuariosDto usuarioDto, string token)
        {
            var usuariosViewModel = new UsuariosViewModel();
            var roles = await _baseApi.GetToApi("Roles/BuscarRoles", token);
            var resultadoRoles = roles as OkObjectResult;

            if (usuarioDto != null)
            {
                usuariosViewModel = usuarioDto;
            }

            if (resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<Roles>>(resultadoRoles.Value.ToString());
                var listaItemsRoles = new List<SelectListItem>();
                foreach (var list in listaRoles)
                {
                    listaItemsRoles.Add(new SelectListItem { Text = list.Nombre, Value = list.Id.ToString() });
                }
                usuariosViewModel.Lista_Roles = listaItemsRoles;
            }

            return usuariosViewModel;
        }

        public async Task<Usuarios> BuscarUsuario(LoginDto loginDto)
        {
            return await _manager.BuscarUsuarioAsync(loginDto);
        }
    }
}
