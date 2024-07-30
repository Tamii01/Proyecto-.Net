using Data.Base;
using Data.Dtos;
using Data.Entities;
using Data.Manager;
using Common.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoIt.Services;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using ProyectoIt.ViewModels;

namespace ProyectoIt.Controllers
{
	public class LoginController : Controller
	{

		private readonly IConfiguration _configuration;
		private readonly LoginService _loginService;

		public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration )
		{
			_configuration = configuration;
            _loginService = new LoginService(httpClientFactory, configuration);
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

			var responseLogin = await _loginService.GuardarUsuario(crearUsuarioDto, HttpContext.Session.GetString("Token"));

			if (responseLogin != null && Convert.ToBoolean(responseLogin.Value) == true)
			{
				TempData["ErrorLogin"] = "Se creo el usuario correctamente";
			}
			else
			{
                TempData["ErrorLogin"] = "No se pudo crear el usuario. Contacte a sistemas";
            }
			return RedirectToAction("Login", "Login");
		}

		public async Task<ActionResult> LoginLocal(LoginDto login)
		{
			var resultadoLogin = await _loginService.ObtenerToken(login);
			
			if (resultadoLogin != null)
			{
                var resultadoSplit = resultadoLogin.Value.ToString().Split(";");
                var usuarioPrincipal = await _loginService.ClaimLogin(login, resultadoSplit);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, usuarioPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddYears(1),
                });

                HttpContext.Session.SetString("Token", resultadoSplit[0]);

				var homeViewModel = new HomeViewModel();
				homeViewModel.Token = resultadoSplit[0];
				homeViewModel.AjaxUrl = _configuration["Url:Api"];
				return View("~/Views/Home/Index.cshtml", homeViewModel);
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}

		}


		public async Task LoginGoogle()
		{

			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = Url.Action("GoogleResponse")
			});await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = Url.Action("GoogleResponse")
			});
		}

		public async Task<ActionResult> GoogleResponse()
		{
			
			var resultado = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			var claims = resultado.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
			{
				claim.Value
			});

            var usuario = _loginService.BuscarUsuario(claims.ToList()[4].Value);

			if(usuario != null)
			{
                return RedirectToAction("Index", "Home");
			}
			else
			{
                return RedirectToAction("Login", "Login");
            }
            
		}

		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
			_loginService.EnviarMail(loginDto);
            TempData["Mail"] = loginDto.Mail;
			return RedirectToAction("RecuperarCuenta", "Login"); 
		}


		public async Task<ActionResult> CambiarClave (LoginDto loginDto)
		{
			var resultadoCuenta = await _loginService.CambiarClave(loginDto, TempData["Mail"].ToString());
			if (resultadoCuenta)
			{
                TempData["ErrorLogin"] = "Se ha cambiado la clave correctamente";
			}
			else
			{
				TempData["ErrorLogin"] = "El codigo ingresado no coincide con el enviado al mail";
            }

            return RedirectToAction("Login", "Login");

        }
    }
}
