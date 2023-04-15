using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        private readonly IQueryGetInfoLote _IQueryGetInfoLote;

        public ProductWebServiceController(ICommandRegProduct ICommandRegProduct,
                                           IQueryGetInfoLote IQueryGetInfoLote)
        {
            _ICommandRegProduct = ICommandRegProduct;
            _IQueryGetInfoLote = IQueryGetInfoLote;
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

        [HttpGet("GetInfoLote")]
        public async Task<List<InformeLote>> GetInfoLote(int NumLote)
        {

            return _IQueryGetInfoLote.GetInfoLote(NumLote);

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
