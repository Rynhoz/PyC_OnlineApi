using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_OnlineApi.Data;
using P_OnlineApi.Models;

namespace P_OnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly PedidoDbContext _context;

        public ProductoController(PedidoDbContext context) => _context = context;

        [HttpGet]
        public ActionResult<Pedido> GetProductos()
        {
            var productos = _context.Productos.ToList();
            if (productos == null) return NoContent();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var prod = _context.Clientes.FirstOrDefault(p => p.Id == id);
            if (prod == null) return NoContent();
            return Ok(prod);
        }

        [HttpPost]
        public ActionResult<Producto> CreateProducto(Producto prod)
        {
            if (string.IsNullOrWhiteSpace(prod.Nombre)) return BadRequest("Debe ingresar el Nombre correctamente");
            if (prod.Precio < 0) return BadRequest("Debe ingresar el Precio correctamente");
            if (!prod.Estado) return BadRequest("Debe ingresar el Estado correctamente");
            _context.Productos.Add(prod);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducto), new { id = prod.Id }, prod);
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> UpdateProducto(int id, Producto prod)
        {
            var pr = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (pr == null) return NoContent();
            if (string.IsNullOrWhiteSpace(prod.Nombre)) return BadRequest("Debe ingresar el Nombre correctamente");
            if (prod.Precio < 0) return BadRequest("Debe ingresar el Precio correctamente");
            pr.Nombre = prod.Nombre;
            pr.Precio = prod.Precio;
            pr.Estado = prod.Estado;
            _context.SaveChanges();
            return Ok(_context.Productos);
        }

        [HttpDelete("{id}")]
        public ActionResult<Producto> DeletearProducto(int id)
        {
            var pr = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (pr == null) return NoContent();
            _context.Productos.Remove(pr);
            _context.SaveChanges();
            return Ok(_context.Productos);
        }

        //[HttpPatch("{id}/cambiar-estado")]
        //public async Task<ActionResult> DeleteProducto(int id)
        //{
        //    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        //    if (producto == nameof) return NotFound("El producto no fue encontrado");
        //    producto.Estado = !producto.Estado;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

    }
}
