using Microsoft.AspNetCore.Mvc;

namespace conecta2.Controllers
{
    public class DispositivosController : Controller
    {
        // Estado del foco (true: encendido, false: apagado)
        private static bool _isFocoEncendido = false;

        public IActionResult Dispositivos()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ToggleFoco(bool encender)
        {
            if (encender)
            {
                _isFocoEncendido = true;
                return Json(new { message = "Foco encendido." });
            }
            else
            {
                _isFocoEncendido = false;
                return Json(new { message = "Foco apagado." });
            }
        }
    }
}

