using Data.Base;
using Data.Dtos;
using Data.Manager;
using Microsoft.AspNetCore.Mvc;
using ProyectoIt.Services;

namespace ProyectoIt.Controllers
{
	public class LoginController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;

		public LoginController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;

		}

		public ActionResult CrearCuenta()
		{
			return View();
		}
		public ActionResult Login()
		{
			if (TempData["ErrorLogin"] != null)
			{
				ViewBag.ErrorLogin = TempData["ErrorLogin"].ToString();
			}
			return View();
		}

		public async Task<ActionResult> CrearUsuario(CrearCuentaDto crearUsuarioDto)
		{
			var baseApi = new BaseApi(_httpClientFactory);
			var response = await baseApi.PostToApi("Usuarios/CrearUsuario", crearUsuarioDto);

			var responseLogin = response as OkObjectResult;
			if (responseLogin != null && Convert.ToBoolean(responseLogin.Value) == true)
			{
				TempData["ErrorLogin"] = "Se creo el usuario correctamente";
				return RedirectToAction("Login", "Login");
			}

			TempData["ErrorLogin"] = "No se pudo crear el usuario. Contacte a sistemas";
			return RedirectToAction("Login", "Login");


		}

		public ActionResult LoginLocal(LoginDto login)
		{
			var usuariosManager = new UsuariosManager();
			var usuarios = usuariosManager.BuscarAsync(login);
			if (usuarios.Result != null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}

		}


		public ActionResult LoginGoogle()
		{
			var usuariosManager = new UsuariosManager();
			var usuarios = usuariosManager.BuscarListaAsync();
			if (usuarios.Result.Count > 0)
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

		public async Task<ActionResult> EnviarMail(LoginDto loginDto)
		{

			var guid = Guid.NewGuid();
			 var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
			var seed = int.Parse(numeros.Substring(0, 6));
			var random = new Random(seed);
			var codigo = random.Next(000000, 999999);

			var recuperarCuenta = new RecuperarCuentaService();
			var usuario = await recuperarCuenta.BuscarUsuarios(loginDto);
			if(usuario != null)
			{
				usuario.Codigo = codigo;
				recuperarCuenta.GuardarUsuario(usuario);
			} 
			return RedirectToAction("RecuperarCuenta", "Login"); 
		}
	}
}
