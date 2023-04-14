using Microsoft.AspNetCore.Mvc;
using WebStock.DB;
using WebStock.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStock.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWebServiceController : ControllerBase
    {

        private readonly ICommandRegProduct _ICommandRegProduct;

        public ProductWebServiceController(ICommandRegProduct ICommandRegProduct)
        {
            _ICommandRegProduct = ICommandRegProduct;
        }

        [HttpGet("GetProductos")]
        public async Task<List<string>> GetProducts() {

            return new List<string>() { "ATUNES TESORO DEL MAR","MASA JUANA","PASTA ROMA"};
        
        }

        [HttpGet("GetProvedores")]
        public async Task<List<string>> GetProvedores()
        {

            return new List<string>() { "MAYCA", "EL ARREO", "QUESOS DON BETO" };

        }


        [HttpPost]
        public string RegProduct([FromBody] Product product)
        {
            if (product != null)
            {
                return _ICommandRegProduct.InsertProduct(product);
            }
            else
            {
                return "Error al procesar la entrada, el producto ingreso como Nulo!";
            }
        }

        
    }
}
