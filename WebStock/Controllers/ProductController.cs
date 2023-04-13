using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            return View(new Product());
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                HttpResponseMessage response = await _HttpClient.PostAsJsonAsync($"{APIUrl}ProductWebService/", product);
                response.EnsureSuccessStatusCode();

                return View("Products",new Product());
            }
            catch
            {
                return View();
            }
        }


     
    }
}
