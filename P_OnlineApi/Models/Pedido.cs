using System.Text.Json.Serialization;

namespace P_OnlineApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        //se tiene que colocar el nombre de esta forma para que se reconozca 
        public int ClienteId { get; set; }
        //para que entityframework entienda que es una relacion se tiene que crear el objeto de la relacion a la va a relacionar la tabla
        [JsonIgnore] //Notacion (serializacion es convertirlo a Json) IMPORTANTE
        public Cliente? Cliente { get; set; } 

        public string Detalle { get; set; } = string.Empty;
        public int Total { get; set; }
        public string Estado { get; set; } = string.Empty;

        //public string NombreCliente { get; set; } = string.Empty;
        //public string Direccion { get; set; } = string.Empty;
        //public string Telefono { get; set; } = string.Empty;
    }
}
