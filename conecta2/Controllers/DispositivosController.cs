using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;


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
            _httpClient.BaseAddress = new Uri("http://192.168.0.123:32772/");

            StringContent content = new StringContent(JsonConvert.SerializeObject(encender), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"dispositivos/{circuito}", content);

            response.EnsureSuccessStatusCode();

            _circuitosEstado[1] = true;

            _circuitosEstado[circuito] = encender;

            string mensaje = $"Circuito {circuito} {(encender ? "encendido" : "apagado")}.";

            return Json(new { message = mensaje });
        }
    }
}



