using ESP8266.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace conecta2.Controllers
{
    [Authorize]
    public class DispositivosController : Controller
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("http://192.168.1.123:32772/api/") };

        [HttpPost]
        public async Task<JsonResult> ToggleFoco(int idDispositivo, bool encender)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(!encender), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"dispositivos/{idDispositivo}", content);

            response.EnsureSuccessStatusCode();

            string mensaje = $"Dispositivo: {idDispositivo} {(encender ? "encendido" : "apagado")}.";

            return Json(new { message = mensaje });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerFoco(int idDispositivo)
        {
            HttpResponseMessage response = await client.GetAsync($"dispositivos/{idDispositivo}");
            response.EnsureSuccessStatusCode();

            string message = await response.Content.ReadAsStringAsync();

            var foco = JsonConvert.DeserializeObject<Dispositivo>(message);

            if (foco != null)
            {
                string mensaje = $"Dispositivo: {foco.IdDispositivo} {(foco.Estado ? "apagado" : "encendido")}.";

                return Json(new { message = mensaje });
            }
            else
            {
                return Json(new { message = "No se encontró el dispositivo" });
            }
        }

        public async Task<IActionResult> ConsumirApi1()
        {
            try
            {
                // Ejemplo con una API real (https://jsonplaceholder.typicode.com/)
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "API 1 consumida correctamente" });
                }
                else
                {
                    return Json(new { success = false, message = $"Error al consumir API 1. Código de estado: {response.StatusCode}" });
                }
            }
            catch (Exception ex)
            {
                // Captura de errores generales
                return Json(new { success = false, message = $"Excepción al consumir API 1: {ex.Message}" });
            }
        }

        // Método para simular la API 2
        public async Task<IActionResult> ConsumirApi2()
        {
            // Aquí simulas el consumo de una API ficticia
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts"); // Ejemplo de API real

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "API 2 consumida correctamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al consumir API 2" });
            }
        }




        //// Estado del foco (true: encendido, false: apagado)
        //private static bool _isFocoEncendido = false;

        //private HttpClient _httpClient = new();

        //// Estado de los circuitos (true: encendido, false: apagado)
        //private static Dictionary<int, bool> _circuitosEstado = new Dictionary<int, bool>
        //{
        //    { 1, false }, // Circuito 1 apagado
        //    { 2, false }, // Circuito 2 apagado
        //    { 3, false }, // Circuito 3 apagado
        //    { 4, false }  // Circuito 4 apagado
        //};

        public IActionResult Dispositivos()
        {
            return View();
        }
    }
}



