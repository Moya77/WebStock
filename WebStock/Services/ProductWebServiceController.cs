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


        [HttpPost]
        public void RegProduct([FromBody] Product product)
        {
            if (product != null)
            {
            _ICommandRegProduct.InsertProduct(product);
            }
        }

        
    }
}
