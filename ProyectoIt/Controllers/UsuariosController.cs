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
        private readonly IHttpClientFactory _httpClientFactory;
        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Usuarios()
        {
            return View();
        }


        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuariosDto usuarioDto)
        {
            var usuariosViewModel = new UsuariosViewModel();
            var baseApi = new BaseApi(_httpClientFactory);
            var roles = await baseApi.GetToApi("Roles/BuscarRoles");
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
            var baseApi = new BaseApi(_httpClientFactory);
            await baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto);
            return RedirectToAction("Usuarios", "Usuarios");
        }

        public async Task<IActionResult> EliminarUsuario([FromBody] UsuariosDto usuarioDto)
        {
            var baseApi = new BaseApi(_httpClientFactory);
            usuarioDto.Activo = false;
            await baseApi.PostToApi("Usuarios/CrearUsuario", usuarioDto);
            return RedirectToAction("Usuarios", "Usuarios");
        }
    }
}
