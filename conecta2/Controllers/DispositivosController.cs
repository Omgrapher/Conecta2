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
            try
            {
                // Invertir el valor de encender
                StringContent content = new StringContent(JsonConvert.SerializeObject(!encender), Encoding.UTF8, "application/json");

                // Hacer la solicitud PUT a la API externa
                HttpResponseMessage response = await client.PutAsync($"dispositivos/{idDispositivo}", content);

                // Asegurarse de que la solicitud haya sido exitosa
                response.EnsureSuccessStatusCode();

                // Crear mensaje de éxito
                string mensaje = $"Dispositivo: {idDispositivo} {(encender ? "encendido" : "apagado")}.";
                return Json(new { message = mensaje });
            }
            catch (Exception ex)
            {
                // Capturar y devolver el error como JSON para manejarlo en el frontend
                return Json(new { error = true, message = ex.Message });
            }
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



