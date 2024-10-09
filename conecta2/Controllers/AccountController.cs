using Microsoft.AspNetCore.Mvc;
using conecta2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

                    TempData["SuccessMessage"] = "Inicio de sesión exitoso!";

                    return RedirectToAction("homePage", "Home");
                }

                //ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                TempData["ErrorMessage"] = "Credenciales inválidas.";
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
