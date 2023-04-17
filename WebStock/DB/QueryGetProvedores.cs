using System.Data;
using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface IQueryGetProvedores
    {
        public List<string> GetProvedores();
    }
    public class QueryGetProvedores : IQueryGetProvedores
    {
        private readonly IConfiguration _IConfiguration;


        public QueryGetProvedores(IConfiguration IConfiguration) {
            _IConfiguration = IConfiguration;
        }

        public List<string> GetProvedores()
        {
            try
            {
                List<string> provedores= new List<string>();
                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("GetProvedores", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read()) {

                                provedores.Add(reader["Nombre"].ToString());
                            }
                            return provedores;
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
