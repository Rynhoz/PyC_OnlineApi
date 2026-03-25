namespace P_OnlineApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int Telefono { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
