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
        private readonly ICommandRegSalidaProducto _ICommandRegSalidaProducto;
        private readonly IQueryGetFaltantes _IQueryGetFaltantes;
        private readonly IQueryGetProvedores _IQueryGetProvedores;
        private readonly IQueryGetProductos _IQueryGetProductos;

        public ProductWebServiceController(ICommandRegProduct ICommandRegProduct,
                                           IQueryGetInfoLote IQueryGetInfoLote,
                                           ICommandRegSalidaProducto iCommandRegSalidaProducto,
                                           IQueryGetFaltantes iQueryGetFaltantes,
                                           IQueryGetProvedores iQueryGetProvedores,
                                           IQueryGetProductos iQueryGetProductos)
        {
            _ICommandRegProduct = ICommandRegProduct;
            _IQueryGetInfoLote = IQueryGetInfoLote;
            _ICommandRegSalidaProducto = iCommandRegSalidaProducto;
            _IQueryGetFaltantes = iQueryGetFaltantes;
            _IQueryGetProvedores = iQueryGetProvedores;
            _IQueryGetProductos = iQueryGetProductos;
        }

        [HttpGet("GetProductos")]
        public async Task<List<string>> GetProducts() {

            return _IQueryGetProductos.GetProductos();


        }

        [HttpGet("GetProvedores")]
        public async Task<List<string>> GetProvedores()
        {

            return _IQueryGetProvedores.GetProvedores();

        }

        [HttpGet("GetInfoLote")]
        public async Task<List<InformeLote>> GetInfoLote(int NumLote)
        {

            return _IQueryGetInfoLote.GetInfoLote(NumLote);

        }

        [HttpGet("GetFaltantes")]
        public async Task<List<InformeLote>> GetFaltantes(int NumLote)
        {

            return _IQueryGetFaltantes.GetFaltantes();

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

        [HttpPost("MoverProducto")]
        public string MoverProducto([FromBody] SalidaInventario salida)
        {
            if (salida != null)
            {
                return _ICommandRegSalidaProducto.RegSalidaProducto(salida);
            }
            else
            {
                return "Error al procesar la entrada, el producto ingreso como Nulo!";
            }
        }


    }
}
