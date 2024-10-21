using Microsoft.AspNetCore.Mvc;
using conecta2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using conecta2.Util;
using System.Reflection.Metadata;
using conecta2.BD;

namespace conecta2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios
                    .FirstOrDefault(u => u.username == model.Username && u.password == model.Password);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, model.Username)
                    };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true // La cookie será persistente (no se elimina al cerrar el navegador)
                        };

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                        string redirectUrl = Url.Action("HomePage", "Home");

                        string swalScript = Swal.Fire("Inicio de Sesión Correcto", "Bienvenido", SwalIcon.Success, redirectUrl);

                        ViewBag.SwalScript = swalScript;

                        return View();


                    }
                    else
                    {
                        string swalScript = Swal.Fire("Usuario o Contraseña Incorrectas", "Inicio Invalido", SwalIcon.Error);

                        ViewBag.SwalScript = swalScript;

                        return View();
                    }

                
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
