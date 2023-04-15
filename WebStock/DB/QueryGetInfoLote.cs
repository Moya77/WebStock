using System.Data;
using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface IQueryGetInfoLote
    {
        public List<InformeLote> GetInfoLote(int numLote);
    }
    public class QueryGetInfoLote : IQueryGetInfoLote
    {
        private readonly IConfiguration _IConfiguration;


        public QueryGetInfoLote(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public List<InformeLote> GetInfoLote(int numLote)
        {
            try
            {
                List<InformeLote> lotes= new List<InformeLote>();
                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("GetInfoLote", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Num_lote", numLote);
                   
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read()) {

                                lotes.Add(new InformeLote()
                                {
                                    
                                    Producto = reader["Nombre"].ToString(),
                                    Cantidad = Int32.Parse(reader["Cantidad"].ToString()),
                                    Expiracion = reader["Fecha_expiracion"].ToString(),
                                    Lote = Int32.Parse(reader["Numero_lote"].ToString()),
                                    Provedor = reader["Nombre_provedor"].ToString(),
                                    Fabricacion = reader["Fecha_fabricacion"].ToString()
                                });
                            }
                            return lotes;
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
