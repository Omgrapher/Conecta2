using Microsoft.AspNetCore.Mvc;
using conecta2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using conecta2.Util;
using System.Reflection.Metadata;
using conecta2.BD;
using ESP8266.Models;

namespace conecta2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public AccountController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://192.168.1.123:32772/api/"); 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.GetAsync($"usuarios?nombre={model.Username}&clave={model.Password}");

                if (response.IsSuccessStatusCode)
                {
                    var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

                    if (usuario != null)
                    {
                        var claims = new List<Claim>
                        {
                           new Claim(ClaimTypes.Name, model.Username)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        string redirectUrl = Url.Action("HomePage", "Home");
                        string swalScript = Swal.Fire("Inicio de Sesión Correcto", "Bienvenido", SwalIcon.Success, redirectUrl);
                        ViewBag.SwalScript = swalScript;

                        return View();
                    }
                }

                string swalScriptError = Swal.Fire("Usuario o Contraseña Incorrectas", "Inicio Invalido", SwalIcon.Error);
                ViewBag.SwalScript = swalScriptError;

                return View();
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
