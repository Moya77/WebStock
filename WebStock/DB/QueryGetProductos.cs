using System.Data;
using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface IQueryGetProductos
    {
        public List<string> GetProductos();
    }
    public class QueryGetProductos : IQueryGetProductos
    {
        private readonly IConfiguration _IConfiguration;


        public QueryGetProductos(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public List<string> GetProductos()
        {
            try
            {
                List<string> productos= new List<string>();
                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("GetProductos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read()) {

                                productos.Add(reader["Nombre"].ToString());
                            }
                            return productos;
                        }
                          
                    }
                }
                
            }catch(Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
