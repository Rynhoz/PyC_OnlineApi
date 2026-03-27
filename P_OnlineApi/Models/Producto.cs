namespace P_OnlineApi.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; } = decimal.Zero;
        public bool Estado { get; set; } = true;
    }
}
