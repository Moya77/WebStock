using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface ICommandRegProduct
    {
        public string InsertProduct(Product product);
    }
    public class CommandRegProduct: ICommandRegProduct
    {
        private readonly IConfiguration _IConfiguration;


        public CommandRegProduct(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public string InsertProduct(Product product)
        {
            try
            {

                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("RegIngresoInventario", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Producto", product.Producto);
                        cmd.Parameters.AddWithValue("@NumeroLote", product.NumeroLote);
                        cmd.Parameters.AddWithValue("@Cantidad", product.Cantidad);
                        cmd.Parameters.AddWithValue("@FechaFabricacion", product.FechaFabricacion);
                        cmd.Parameters.AddWithValue("@Provedor", product.Provedor);
                        cmd.Parameters.AddWithValue("@FechaCaducidad", product.FechaCaducidad);

                        cmd.ExecuteNonQuery();
                    }
                }
                return "El producto se registro de forma correcta!";
            }catch(Exception ex)
            {
                return "Error de registro del producto!";
            }
        }
    }
}
