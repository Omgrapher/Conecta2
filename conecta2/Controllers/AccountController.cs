using Microsoft.AspNetCore.Mvc;
using conecta2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using conecta2.Util;
using System.Reflection.Metadata;

namespace conecta2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
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
                // Validar el usuario (esto debería hacerse contra una base de datos)
                if (model.Username == "admin" && model.Password == "1234")
                {
                    // Crear una lista de claims (información sobre el usuario)
                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, model.Username)
                    };

                    // Crear la identidad del usuario y la cookie de autenticación
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // La cookie será persistente (no se elimina al cerrar el navegador)
                    };

                    // Firmar al usuario (crea la cookie de autenticación)
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();


                    // Generar la URL de redirección
                    string redirectUrl = Url.Action("HomePage", "Home");

                    string swalScript = Swal.Fire("Inicio de Sesión Correcto", "Bienvenido",SwalIcon.Success,redirectUrl);

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
