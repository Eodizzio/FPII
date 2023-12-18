using Microsoft.AspNetCore.Mvc;
using Backend.Dominio;
using Backend.Implementacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        DaoFacturas dao = new DaoFacturas();
        // GET: api/<FacturasController>
        [HttpGet, Route("GetProductos")]
        public IEnumerable<Producto> Get()
        {
            return dao.GetProductos();
        }

        //POST api/<FacturasController>
        [HttpPost, Route("PostFacturas")]
        public IActionResult PostFacturas(Factura oFactura)
        {
            try
            {
                if (oFactura == null)
                    return BadRequest("Datos de Factura Incorrectos");
                return Ok(dao.Save(oFactura));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente luego");
            }
        }
    }
}
