using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
