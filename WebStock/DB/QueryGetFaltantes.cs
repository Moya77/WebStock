using System.Data;
using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface IQueryGetFaltantes
    {
        public List<InformeLote> GetFaltantes();
    }
    public class QueryGetFaltantes : IQueryGetFaltantes
    {
        private readonly IConfiguration _IConfiguration;


        public QueryGetFaltantes(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public List<InformeLote> GetFaltantes()
        {
            try
            {
                List<InformeLote> faltantes= new List<InformeLote>();
                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("GetFaltantes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read()) {

                                faltantes.Add(new InformeLote()
                                {
                                    
                                    Producto = reader["Nombre"].ToString(),
                                    Cantidad = Int32.Parse(reader["Cantidad"].ToString()),
                                    Expiracion = reader["Fecha_expiracion"].ToString(),
                                    Lote = Int32.Parse(reader["Numero_lote"].ToString()),
                                    Provedor = reader["Nombre_provedor"].ToString(),
                                    Fabricacion = reader["Fecha_fabricacion"].ToString()
                                });
                            }
                            return faltantes;
                        }
                          
                    }
                }
                
            }catch(Exception ex)
            {
                return new List<InformeLote>();
            }
        }
    }
}
