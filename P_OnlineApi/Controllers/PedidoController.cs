using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using P_OnlineApi.Data;
using P_OnlineApi.Models;
using P_OnlineApi.Data;
using P_OnlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P_OnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        
        private readonly PedidoDbContext _context; 
        
        public PedidoController(PedidoDbContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        public ActionResult<Pedido> GetPedidos()
        {
            var pedidos = _context.Pedidos.ToList();
            if (pedidos == null) return NoContent();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            var pedido = _context.Pedidos.Include(pdd => pdd.Detalles).FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NoContent();
            return Ok(pedido);
        }

        [HttpPost]
        public ActionResult<Pedido> CreatePedido(Pedido pdd, DetallePedido dtp)
        {
            var produc = _context.Productos.FirstOrDefault(c => c.Id == dtp.ProductoId);
            if (produc == null) return NoContent();
            if (pdd.Total != 0) return BadRequest("No Debe ingresar el monto total ");
            if (string.IsNullOrWhiteSpace(pdd.Estado)) return BadRequest("Debe ingresar el Estado correctamente");
          
            dtp.PrecioUnitario = produc.Precio;
            dtp.SubTotal = produc.Precio * dtp.Cantidad;
            _context.DetallePedido.Add(dtp);
            pdd.Total = pdd.Detalles.Sum(d => d.SubTotal) + dtp.SubTotal;
            _context.Pedidos.Add(pdd);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedido), new { id = pdd.Id }, pdd);
            
        }

        [HttpPost("{id}/detalle")]
        public ActionResult<Pedido> CreateDetallePedido(int id, DetallePedido detail)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == detail.PedidoId);
            var produc = _context.Productos.FirstOrDefault(c => c.Id == detail.ProductoId);
            if (pedido == null || produc == null) return NoContent();
            if (detail.Cantidad <=0) return BadRequest("Debe ingresar la cantidad correctamente o que sea mayor a 0");
            if (detail.Cantidad > produc.Stock) return BadRequest("No hay stock suficiente");
            produc.Stock -= detail.Cantidad; 
            detail.PrecioUnitario = produc.Precio;
            detail.SubTotal = produc.Precio * detail.Cantidad;
            _context.DetallePedido.Add(detail);

            pedido.Total = pedido.Detalles.Sum(d => d.SubTotal) + detail.SubTotal;
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public ActionResult<Pedido> UpdatePedido(int id, Pedido pdd)
        {
            var ped = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (ped == null) return NotFound("ID no encontrado");
            
            if (pdd.Total <= 0) return BadRequest("Debe ingresar el Monto Total correctamente");
            if (string.IsNullOrWhiteSpace(pdd.Estado)) return BadRequest("Debe ingresar el Estado correctamente");
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