using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Data.Base;
using Common.Helpers;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ProyectoIt.ViewModels;
using System.Security.Claims;
using ProyectoIt.Interfaces;

namespace ProyectoIt.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;
        private readonly BaseApi _baseAPi;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient();
            _baseAPi = new BaseApi(httpClientFactory);
            _httpClientFactory = httpClientFactory;
        }
        public async Task<OkObjectResult> ObtenerToken(LoginDto login)
        {
            var token = await _baseAPi.PostToApi("Authenticate/Login", login);
            var resultadoLogin = token as OkObjectResult;

            return resultadoLogin;
        }

        public async Task<OkObjectResult> GuardarUsuario(CrearCuentaDto crearUsuarioDto, string token)
        {
            var response = await _baseAPi.PostToApi("Usuarios/CrearUsuario", crearUsuarioDto, token);
            var responseLogin = response as OkObjectResult;

            return responseLogin;
        }

        public async Task<ClaimsPrincipal> ClaimLogin(LoginDto login, string[] resultadoSplit)
        {
            var token = await _baseAPi.PostToApi("Authenticate/Login", login);
            var resultadoLogin = token as OkObjectResult;
            var usuarioPrincipal = new ClaimsPrincipal();
            if (resultadoSplit.Length > 0)
            {

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new(ClaimTypes.Name, resultadoSplit[1]);
                Claim claimRole = new(ClaimTypes.Role, resultadoSplit[2]);
                Claim claimEmail = new(ClaimTypes.Email, resultadoSplit[3]);

                identity.AddClaim(claimNombre);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                usuarioPrincipal.AddIdentity(identity);

            }

            return usuarioPrincipal;
        }

        public async Task<Usuarios?> BuscarUsuario(string mail)
        {
            var login = new LoginDto();
            login.Mail = mail;

            var usuarioServicio = new UsuariosService(_httpClientFactory);
            var usuario = usuarioServicio.BuscarUsuario(login).Result;

            return usuario;

        }

        public async Task<bool> CambiarClave(LoginDto loginDto, string mail)
        {
            loginDto.Mail = mail;
            var recuperarCuenta = new RecuperarCuentaService();
            var usuario = await recuperarCuenta.BuscarUsuarios(loginDto);
            var resultadoCuenta = false;
            if (usuario != null)
            {
                var usuarioDto = new UsuariosDto();
                usuarioDto = usuario;
                usuarioDto.Codigo = null;
                usuarioDto.Clave = EncryptHelper.Encriptar(loginDto.Password);
                resultadoCuenta = recuperarCuenta.GuardarUsuario(usuarioDto);
            }

            return resultadoCuenta;

        }

     
        public async void EnviarMail(LoginDto loginDto)
        {
            var recuperarCuenta = new RecuperarCuentaService();
            var resultadoCuenta = false;
            var guid = Guid.NewGuid();
            var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(numeros.Substring(0, 6));
            var random = new Random(seed);
            var codigo = random.Next(000000, 999999);

            var usuario = await recuperarCuenta.BuscarUsuarios(loginDto);
            if (usuario != null)
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
            }
        }

        private static string CuerpoMail(int codigo)
        {
            var mensaje = "<strong>A continuacion se mostrara un codigo que debera ingresar en la web de Educacion IT </strong>";
            mensaje += $"<strong>{codigo}</strong><br>";
            return mensaje;
        }
    }
}
