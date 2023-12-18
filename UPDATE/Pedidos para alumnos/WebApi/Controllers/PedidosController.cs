using Backend.Dominio;
using Backend.Implementación;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        DaoPedidos dao = new DaoPedidos();
        // GET: api/<PedidosController>
        [HttpGet, Route("GetClientes")]
        public IEnumerable<Cliente> GetClientes()
        {
            return dao.GetClientes();
        }

        // GET api/<PedidosController>/5
        [HttpGet, Route("GetPedidos/{id}/{Desde}/{Hasta}")]
        public IEnumerable<Pedido> GetPedidos(int id, DateTime Desde, DateTime Hasta)
        {
            return dao.GetPedidos(id, Desde, Hasta);
        }

        // PUT api/<PedidosController>/5
        [HttpPut, Route("PutPedidos/{id}/{opcion}")]
        public IActionResult PutPedidos(int id, int opcion)
        {
            if (dao.UpdatePedidos(id, opcion) != 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}
