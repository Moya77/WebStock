using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class SalidasController : Controller
    {

        private readonly HttpClient _HttpClient;
        private readonly string APIUrl;

        public SalidasController(IConfiguration IConfiguration, HttpClient HttpClient) {
            APIUrl = IConfiguration.GetValue<string>("APIUrl");
            _HttpClient = HttpClient;
        }

        public ActionResult Salidas()
        {
            return View(new ModelSalidaProducto());
        }

        [HttpPost]
        public async Task<IActionResult> MoverProducto(ModelSalidaProducto SalidaProducto)
        {
            try
            {
                HttpResponseMessage response = await _HttpClient.PostAsJsonAsync($"{APIUrl}ProductWebService/MoverProducto", SalidaProducto.Salida);
                response.EnsureSuccessStatusCode();

                string resultado = await response.Content.ReadAsStringAsync();
                ModelSalidaProducto ModeloRepuesta = new();
                ModeloRepuesta.messaje = resultado;
                return View("Salidas", ModeloRepuesta);
            }
            catch
            {
                return View();
            }
        }
    }
}
