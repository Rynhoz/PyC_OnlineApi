using Microsoft.EntityFrameworkCore;
using P_OnlineApi.Models;
using System.Security.Cryptography.X509Certificates;

namespace P_OnlineApi.Data
{
    //Este es el archivo de cotexto que solo tiene que haber uno
    public class PedidoDbContext : DbContext
    {
        public PedidoDbContext(DbContextOptions<PedidoDbContext> options) : base(options) { }
        /// Todas las opciones van a estar instanciadas de acuerdo a la herencia que se use 
        /// DbContextOptions abre las opciones de acuerdo a <PedidoDbContext> 
        /// se hereda de base 


        ///instancia del dataset, osea crear una tabla a partir de codigo 
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