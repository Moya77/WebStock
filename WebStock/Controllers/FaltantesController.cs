using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebStock.Models;

namespace WebStock.Controllers
{

    public class FaltantesController : Controller
    {
        private readonly HttpClient _HttpClient;
        private readonly string APIUrl;

        public FaltantesController(IConfiguration IConfiguration, HttpClient HttpClient) {
            APIUrl = IConfiguration.GetValue<string>("APIUrl");
            _HttpClient = HttpClient;
        }

        public async Task<ActionResult> Faltantes()
        {
            List<InformeLote> faltantes = new List<InformeLote>();
            HttpResponseMessage response = await _HttpClient.GetAsync($"{APIUrl}ProductWebService/GetFaltantes");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            faltantes = JsonConvert.DeserializeObject<List<InformeLote>>(responseBody);

            return View(faltantes);
        }
    }

}
