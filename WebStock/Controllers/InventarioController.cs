using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class InventarioController : Controller
    {

        private readonly HttpClient _HttpClient;
        private readonly string APIUrl;

        public InventarioController(IConfiguration IConfiguration, HttpClient HttpClient)
        {
            APIUrl = IConfiguration.GetValue<string>("APIUrl");
            _HttpClient = HttpClient;
        }
        // GET: InventarioController
        public ActionResult Inventario()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<InformeLote>> GetInfoLote(int numLote)
        {
            HttpResponseMessage response = await _HttpClient.GetAsync($"{APIUrl}ProductWebService/GetInfoLote?NumLote=" + numLote);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            List<InformeLote>? InfoLotes = JsonConvert.DeserializeObject<List<InformeLote>>(responseBody);
            return InfoLotes;
        }
    }
}
