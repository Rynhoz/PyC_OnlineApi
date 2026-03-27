using Microsoft.EntityFrameworkCore;
using P_OnlineApi.Models;
using System.Security.Cryptography.X509Certificates;

namespace P_OnlineApi.Data
{
    //Este es el archivo de cotexto que solo tiene que haber uno
    public class PedidoDbContext : DbContext
    {
        public PedidoDbContext(DbContextOptions<PedidoDbContext> options) : base(options) { }
     
        public DbSet<Pedido> Pedidos { get; set; }


        //Cliente
        public DbSet<Cliente> Clientes { get; set; }

        //Productos
        public DbSet<Producto> Productos { get; set; }

        //Detalle Pedido
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
}
/// como se realiza un inyeccion de dependencias