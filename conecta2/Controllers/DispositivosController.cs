using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;


namespace conecta2.Controllers
{
    [Authorize]
    public class DispositivosController : Controller
    {
        //// Estado del foco (true: encendido, false: apagado)
        //private static bool _isFocoEncendido = false;

        private HttpClient _httpClient = new();

        // Estado de los circuitos (true: encendido, false: apagado)
        private static Dictionary<int, bool> _circuitosEstado = new Dictionary<int, bool>
        {
            { 1, false }, // Circuito 1 apagado
            { 2, false }, // Circuito 2 apagado
            { 3, false }, // Circuito 3 apagado
            { 4, false }  // Circuito 4 apagado
        };

        public IActionResult Dispositivos()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ToggleFoco(int circuito, bool encender)
        {
            string urlBase = "http://192.168.0.16/";

            if (circuito == 1)
            {
                if (encender)
                {
                    await _httpClient.GetAsync(urlBase + "LED1=ON");
                    _circuitosEstado[1] = true;
                }
                else
                {
                    await _httpClient.GetAsync(urlBase + "LED1=OFF");
                    _circuitosEstado[1] = false;
                }
            }
            else if (circuito == 2)
            {
                if (encender)
                {
                    await _httpClient.GetAsync(urlBase + "LED2=ON");
                    _circuitosEstado[2] = true;
                }
                else
                {
                    await _httpClient.GetAsync(urlBase + "LED2=OFF");
                    _circuitosEstado[2] = false;
                }
            }
            else if (circuito == 3)
            {
                if (encender)
                {
                    await _httpClient.GetAsync(urlBase + "LED3=ON");
                    _circuitosEstado[3] = true;
                }
                else
                {
                    await _httpClient.GetAsync(urlBase + "LED3=OFF");
                    _circuitosEstado[3] = false;
                }
            }
            else if (circuito == 4)
            {
                if (encender)
                {
                    await _httpClient.GetAsync(urlBase + "LED4=ON");
                    _circuitosEstado[4] = true;
                }
                else
                {
                    await _httpClient.GetAsync(urlBase + "LED4=OFF");
                    _circuitosEstado[4] = false;
                }
            }

            string mensaje = $"Circuito {circuito} {(encender ? "encendido" : "apagado")}.";
            return Json(new { message = mensaje });
        }
    }
}



