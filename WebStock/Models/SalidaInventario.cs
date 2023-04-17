namespace WebStock.Models
{
    public class SalidaInventario
    {
        public int NumLote { get; set; }
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; }
        public string Fecha_salida { get; set; } = "";
        public string Destino { get; set; } = "";

        public SalidaInventario() { }
    }
}
