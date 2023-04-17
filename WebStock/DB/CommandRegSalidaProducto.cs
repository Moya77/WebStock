using System.Data.SqlClient;
using WebStock.Models;

namespace WebStock.DB
{
    public interface ICommandRegSalidaProducto
    {
        public string RegSalidaProducto(SalidaInventario salida);
    }
    public class CommandRegSalidaProducto : ICommandRegSalidaProducto
    {
        private readonly IConfiguration _IConfiguration;


        public CommandRegSalidaProducto(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }

        public string RegSalidaProducto(SalidaInventario salida)
        {
            try
            {

                using (SqlConnection cn = new(_IConfiguration.GetConnectionString("Connection")))
                {
                    cn.Open();
                    using (SqlCommand cmd = new("RegSalidaInventario", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Producto", salida.Producto);
                        cmd.Parameters.AddWithValue("@NumeroLote", salida.NumLote);
                        cmd.Parameters.AddWithValue("@Cantidad", salida.Cantidad);
                        cmd.Parameters.AddWithValue("@FechaSalida", salida.Fecha_salida);
                        cmd.Parameters.AddWithValue("@Destino", salida.Destino);

                        cmd.ExecuteNonQuery();
                    }
                }
                return "La salida se realizó de forma exitosa!";
            }
            catch (Exception ex)
            {
                return "Error Al intentar realizar el movimiento de producto!";
            }
        }
    }
}
