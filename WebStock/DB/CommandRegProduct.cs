using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface ICommandRegProduct
    {
        public bool InsertProduct(Product product);
    }
    public class CommandRegProduct: ICommandRegProduct
    {
        private readonly IConfiguration _IConfiguration;


        public CommandRegProduct(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public bool InsertProduct(Product product)
        {
            using(SqlConnection cn = new(_IConfiguration.GetConnectionString("DBConnection")))
            {
                cn.Open();
                using(SqlCommand cmd = new("RegProduct", cn))
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
            return true;
        }
    }
}
