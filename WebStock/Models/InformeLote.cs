namespace WebStock.Models
{
    public class InformeLote
    {
        public int Lote { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string Fabricacion { get; set; }
        public string Expiracion { get; set; }
        public string Provedor { get; set; }
        public InformeLote() { }
    }
}
