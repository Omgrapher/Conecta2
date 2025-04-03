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
        public enum EstadoTierra
        {
            Seca = 0,
            Humeda = 1
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerEstadoHumedad(int idDispositivo)
        {
            HttpResponseMessage response = await client.GetAsync($"dispositivos/{idDispositivo}/estado");
            response.EnsureSuccessStatusCode();

            string estadoStr = await response.Content.ReadAsStringAsync();

            int estadoInt = int.Parse(estadoStr);
            
            return Json(new { estado = estadoInt });
        }
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("http://192.168.1.123:32772/api/") };

        [HttpPost]
        public async Task<JsonResult> ToggleFoco(int idDispositivo, bool encender)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(!encender), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"dispositivos/{idDispositivo}", content);

                response.EnsureSuccessStatusCode();

                string mensaje = $"Dispositivo: {idDispositivo} {(encender ? "encendido" : "apagado")}.";
                return Json(new { message = mensaje });
            }
            catch (Exception ex)
            {
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
        
        public IActionResult Dispositivos()
        {
            return View();
        }
    }
}



