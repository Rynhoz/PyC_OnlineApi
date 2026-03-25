namespace P_OnlineApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public int Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        
    }
}
