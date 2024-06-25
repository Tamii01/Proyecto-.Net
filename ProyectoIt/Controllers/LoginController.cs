using Data.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIt.Controllers
{
	public class LoginController : Controller
	{

		public ActionResult CrearCuenta()
		{
			return View();
		}
		public ActionResult Login()
		{
			return View();
		}

		public ActionResult LoginGoogle()
		{
			var usuariosManager = new UsuariosManager();
			var usuarios = usuariosManager.BuscarListaAsync();
			if(usuarios.Result.Count > 0)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
			
		}

		public ActionResult Logout()
		{
			return RedirectToAction("Login", "Login");
		}

		public ActionResult OlvidoClave()
		{
			return View();
		}

		public ActionResult RecuperarCuenta()
		{
			return View();
		}

		public ActionResult EnviarMail()
		{
			return RedirectToAction("RecuperarCuenta", "Login");
		}
	}
}
