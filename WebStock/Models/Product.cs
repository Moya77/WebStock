namespace WebStock.Models
{
    public class Product
    {
        public string? Producto { get; set; } = "";
        public int? NumeroLote { get; set; } = 0;
        public int? Cantidad { get; set; } = 0;
        public string? FechaFabricacion { get; set; } = "";
        public string? Provedor { get; set; } = "";
        public string? FechaCaducidad { get; set; } = "";

        public Product() { }

      
    }
}
