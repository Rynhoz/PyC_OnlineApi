using System.Text.Json.Serialization;

namespace P_OnlineApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
     
        public int ClienteId { get; set; }
        
        [JsonIgnore]
        public Cliente? Cliente { get; set; } 
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<DetallePedido> Detalles { get; set; } = new();

    }
}
