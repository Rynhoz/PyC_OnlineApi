using System.Text.Json.Serialization;

namespace P_OnlineApi.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; }
        public int ProductoId { get; set; }
        [JsonIgnore]
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
