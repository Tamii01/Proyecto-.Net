using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Usuarios()
        {
            return View();
        }

        public IActionResult UsuariosAddPartial()
        {
            return View();
        }
    }
}
