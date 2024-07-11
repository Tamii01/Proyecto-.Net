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

namespace ProyectoIt.Controllers
{
	public class LoginController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly SmtpClient _smtpClient;

		public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration )
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
            _smtpClient = new SmtpClient();
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

		public async Task<ActionResult> LoginLocal(LoginDto login)
		{
			var usuariosManager = new UsuariosManager();
			var usuarios = usuariosManager.BuscarAsync(login);
			if (usuarios.Result != null)
			{
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				Claim claimNombre = new(ClaimTypes.Name, usuarios.Result.Nombre);
				Claim claimRole = new(ClaimTypes.Role, usuarios.Result.Roles.Nombre);
				Claim claimEmail= new(ClaimTypes.Email, usuarios.Result.Mail);

				identity.AddClaim(claimNombre);
				identity.AddClaim(claimRole);
				identity.AddClaim(claimEmail);

				var usuarioPrincipal = new ClaimsPrincipal(identity);
				
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, usuarioPrincipal, new AuthenticationProperties
				{
					ExpiresUtc = DateTime.Now.AddYears(1),
				}); 
				return RedirectToAction("Index", "Home");
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

			var login = new LoginDto();
			login.Mail = claims.ToList()[4].Value;

			var usuarioServicio = new UsuariosService();
			var usuario = usuarioServicio.BuscarUsuario(login).Result;
			
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
            var recuperarCuenta = new RecuperarCuentaService();
            var resultadoCuenta = false;
            var guid = Guid.NewGuid();
			var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
			var seed = int.Parse(numeros.Substring(0, 6));
			var random = new Random(seed);
			var codigo = random.Next(000000, 999999);

			var usuario = await recuperarCuenta.BuscarUsuarios(loginDto);
			if(usuario != null)
			{
				usuario.Codigo = codigo;
                resultadoCuenta = recuperarCuenta.GuardarUsuario(usuario);
			}

			if (resultadoCuenta)
			{
				var mail = new MailMessage();

				mail.From = new MailAddress(_configuration["ConfiguracionMail:Usuario"]);
				mail.To.Add(loginDto.Mail);
				mail.Subject = "Codigo de recuperacion EducacionIT";
				mail.Body = CuerpoMail(codigo);
				mail.IsBodyHtml = true;
				mail.Priority = MailPriority.Normal;

				_smtpClient.Host = _configuration["ConfiguracionMail:DireccionServidor"];
				_smtpClient.Port = int.Parse(_configuration["ConfiguracionMail:Puerto"]);
				_smtpClient.EnableSsl = bool.Parse(_configuration["ConfiguracionMail:Ssl"]);
				_smtpClient.UseDefaultCredentials = false;
				_smtpClient.Credentials = new NetworkCredential(_configuration["ConfiguracionMail:Usuario"], _configuration["ConfiguracionMail:Clave"]);

				_smtpClient.Send(mail);

                TempData["Mail"] = loginDto.Mail;
            }


			return RedirectToAction("RecuperarCuenta", "Login"); 
		}

		private static string CuerpoMail(int codigo)
		{
			var mensaje = "<strong>A continuacion se mostrara un codigo que debera ingresar en la web de Educacion IT </strong>";
			mensaje += $"<strong>{codigo}</strong><br>";
			return mensaje;
		}


		public async Task<ActionResult> CambiarClave (LoginDto loginDto)
		{
			loginDto.Mail = TempData["Mail"].ToString();

            var recuperarCuenta = new RecuperarCuentaService();
			var usuario = await recuperarCuenta.BuscarUsuarios(loginDto);
			var resultadoCuenta = false;
			if(usuario != null)
			{
				var usuarioDto = new UsuariosDto();
				usuarioDto = usuario;
				usuarioDto.Codigo = null;
				usuarioDto.Clave = EncryptHelper.Encriptar(loginDto.Password);
				resultadoCuenta = recuperarCuenta.GuardarUsuario(usuarioDto);
			}

			if (resultadoCuenta)
			{
                TempData["ErrorLogin"] = "Se ha cambiado la clave correctamente";
                return RedirectToAction("Login", "Login");
			}
			else
			{
				TempData["ErrorLogin"] = "El codigo ingresado no coincide con el enviado al mail";
                return RedirectToAction("Login", "Login");
            }

        }
    }
}
