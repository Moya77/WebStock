using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _HttpClient;
        private readonly string APIUrl;

        public ProductController(IConfiguration IConfiguration, HttpClient HttpClient) 
        {
             APIUrl = IConfiguration.GetValue<string>("APIUrl");
            _HttpClient = HttpClient;
        }
        public ActionResult Products()
        {
            return View(new ModelEntradaProducto());
        }

        [HttpGet]

        public async Task<List<string>> ProductsList()
        {
            HttpResponseMessage response = await _HttpClient.GetAsync($"{APIUrl}ProductWebService/GetProductos");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            List<string>? products = JsonConvert.DeserializeObject<List<string>>(responseBody);
            return products;
        }

        public async Task<List<string>> ProvedorList()
        {
            HttpResponseMessage response = await _HttpClient.GetAsync($"{APIUrl}ProductWebService/GetProvedores");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            List<string>? provedores = JsonConvert.DeserializeObject<List<string>>(responseBody);
            return provedores;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ModelEntradaProducto Entradaproduct)
        {
            try
            {
                HttpResponseMessage response = await _HttpClient.PostAsJsonAsync($"{APIUrl}ProductWebService/", Entradaproduct.producto);
                response.EnsureSuccessStatusCode();

                string resultado = await response.Content.ReadAsStringAsync();
                ModelEntradaProducto ModeloRsepuesta = new();
                ModeloRsepuesta.messaje=resultado;
                return View("Products",ModeloRsepuesta);
            }
            catch
            {
                return View();
            }
        }

       
    }
}
