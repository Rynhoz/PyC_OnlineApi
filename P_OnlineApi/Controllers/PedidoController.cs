using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using P_OnlineApi.Data;
using P_OnlineApi.Models;
using P_OnlineApi.Data;
using P_OnlineApi.Models;

namespace P_OnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        //private static readonly List<Pedido> pedidos = PedidoData.pedidos; 

        //este contexto tiene que ser privado si o si
        private readonly PedidoDbContext _context; //decalracion variable de tipo priv
        
        public PedidoController(PedidoDbContext context)
        {
            //cada vez que se inicialice la calse se va a conectar a la base de datos
            _context = context;
        }

        [HttpGet]
        public ActionResult<Pedido> GetPedidos()
        {
            var pedidos = _context.Pedidos.ToList();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NoContent();
            return Ok(pedido);
        }

        [HttpPost]
        public ActionResult<Pedido> CreatePedido(Pedido pdd)
        {
            //if (string.IsNullOrWhiteSpace(pdd.NombreCliente)) return BadRequest("Debe ingresar el Nombre del cliente correctamente");
            //if (string.IsNullOrWhiteSpace(pdd.Direccion)) return BadRequest("Debe ingresar la Direccion correctamente");
            //if (string.IsNullOrWhiteSpace(pdd.Telefono)) return BadRequest("Debe ingresar el Telefono correctamente");
            if (string.IsNullOrWhiteSpace(pdd.Detalle)) return BadRequest("Debe ingresar el Detalle correctamente");
            if (pdd.Total <= 0) return BadRequest("Debe ingresar el Monto Total correctamente");
            if (string.IsNullOrWhiteSpace(pdd.Estado)) return BadRequest("Debe ingresar el Estado correctamente");
            //pdd.Id = pedidos.Max(p => p.Id) + 1;
            _context.Pedidos.Add(pdd);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedido), new { id = pdd.Id }, pdd);
            /// /api/pedido/      8/ "nuevo id"  "Devuelve el pedido recien creado"
        }

        [HttpPut("{id}")]
        public ActionResult<Pedido> UpdatePedido(int id, Pedido pdd)
        {
            var ped = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (ped == null) return NotFound("ID no encontrado");
            //if (string.IsNullOrWhiteSpace(pdd.NombreCliente)) return BadRequest("Debe ingresar el Nombre del cliente correctamente");
            //if (string.IsNullOrWhiteSpace(pdd.Direccion)) return BadRequest("Debe ingresar la Direccion correctamente");
            //if (string.IsNullOrWhiteSpace(pdd.Telefono)) return BadRequest("Debe ingresar el Telefono correctamente");
            if (string.IsNullOrWhiteSpace(pdd.Detalle)) return BadRequest("Debe ingresar el Detalle correctamente");
            if (pdd.Total <= 0) return BadRequest("Debe ingresar el Monto Total correctamente");
            if (string.IsNullOrWhiteSpace(pdd.Estado)) return BadRequest("Debe ingresar el Estado correctamente");
            //ped.NombreCliente = pdd.NombreCliente;
            //ped.Direccion = pdd.Direccion;
            //ped.Telefono = pdd.Telefono;
            ped.Detalle = pdd.Detalle;
            ped.Total = pdd.Total;
            ped.Estado = pdd.Estado;
            _context.SaveChanges();
            return Ok(_context.Pedidos);
        }

        [HttpDelete("{id}")]
        public ActionResult<Pedido> DeletePedido(int id)
        {
            var ped = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (ped == null) return NoContent();
            _context.Pedidos.Remove(ped);
            _context.SaveChanges();
            return Ok(_context.Pedidos);
        }
    }
}