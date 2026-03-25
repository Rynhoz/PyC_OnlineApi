using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P_OnlineApi.Data;
using P_OnlineApi.Models;

namespace P_OnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly PedidoDbContext _context;

        public ClienteController(PedidoDbContext context) => _context = context;
        

        [HttpGet]
        public ActionResult<Cliente> GetClientes()
        {
            var pedidos = _context.Clientes.ToList();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NoContent();
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> CreateCliente(Cliente cli)
        {
            if (string.IsNullOrWhiteSpace(cli.NombreCompleto)) return BadRequest("Debe ingresar el Nombre completo correctamente");
            if (cli.Telefono < 0) return BadRequest("Debe ingresar el Telefono correctamente");
            if (string.IsNullOrWhiteSpace(cli.Direccion)) return BadRequest("Debe ingresar la Direccion correctamente");
            if (string.IsNullOrWhiteSpace(cli.Correo) || !cli.Correo.Contains("@")) return BadRequest("Debe ingresar el Correo correctamente");
            if (!cli.Activo) return BadRequest("Debe ingresar el Estado correctamente");
            _context.Clientes.Add(cli); 
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCliente), new { id = cli.Id }, cli);
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> UpdateCliente(int id, Cliente cli)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NoContent();
            if (string.IsNullOrWhiteSpace(cli.NombreCompleto)) return BadRequest("Debe ingresar el Nombre completo correctamente");
            if (cli.Telefono < 0) return BadRequest("Debe ingresar el Telefono correctamente");
            if (string.IsNullOrWhiteSpace(cli.Direccion)) return BadRequest("Debe ingresar la Direccion correctamente");
            if (string.IsNullOrWhiteSpace(cli.Correo)) return BadRequest("Debe ingresar el Correo correctamente");
            //if (cli.Activo is int) return BadRequest("Debe ingresar el Estado correctamente");
            cliente.NombreCompleto = cli.NombreCompleto;
            cliente.Telefono = cli.Telefono;
            cliente.Direccion = cli.Direccion;
            cliente.Correo = cli.Correo;
            cliente.Activo = cli.Activo;
            _context.SaveChanges();
            return Ok(_context.Clientes);
        }

        [HttpDelete("{id}")]
        public ActionResult<Cliente> DeleteCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NoContent();
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok(_context.Clientes);
        }

        [HttpGet("buscar")]
        public ActionResult<Cliente> SearchCliente(string? nombre, bool? activo)
        {
            var query = _context.Clientes.AsQueryable(); //Convierte la lista en un query de db

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(c => c.NombreCompleto.ToLower().Contains(nombre.ToLower()));
            }
            if (activo.HasValue)
            {
                query = query.Where(c => c.Activo == activo);
            }

            var resultados = query.ToList();

            if (resultados.Count == 0) return NoContent();

            return Ok(resultados);
        }
    }
}
