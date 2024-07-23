using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoIt.ViewModels;

namespace ProyectoIt.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly BaseApi _baseApi;
        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _baseApi = new BaseApi(httpClientFactory);
        }

        public IActionResult Usuarios()
        {
            return View();
        }


        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuariosDto usuarioDto)
        {
            var usuariosViewModel = new UsuariosViewModel();
            var roles = await _baseApi.GetToApi("Roles/BuscarRoles");
            var resultadoRoles = roles as OkObjectResult;

            if(usuarioDto != null)
            {
                usuariosViewModel = usuarioDto;
            }

            if (resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<Roles>>(resultadoRoles.Value.ToString());
                var listaItemsRoles = new List<SelectListItem>();
                foreach (var list in listaRoles)
                {
                    listaItemsRoles.Add(new SelectListItem {Text = list.Nombre, Value = list.Id.ToString() });
                }
                usuariosViewModel.Lista_Roles = listaItemsRoles;
            }

            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuariosViewModel);
        }

        public async Task<IActionResult> GuardarUsuario(UsuariosDto usuarioDto)
        {
            await _baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto);
            return RedirectToAction("Usuarios", "Usuarios");
        }

        public async Task<IActionResult> EliminarUsuario([FromBody] UsuariosDto usuarioDto)
        {
            usuarioDto.Activo = false;
            await _baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto);
            return RedirectToAction("Usuarios", "Usuarios");
        }
    }
}
