using Backend.Interfaces;
using Backend.Dominio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Singleton;
using Newtonsoft.Json;

namespace Backend.Implementacion
{
    public class Servicio : IServicio
    {
        public async Task<List<Producto>> GetProductos()
        {
            List<Producto> producto = new List<Producto>();
            string content = await ClientSingleton.GetInstance().GetAsync("api/Facturas/GetProductos");
            producto = JsonConvert.DeserializeObject<List<Producto>>(content);
            return producto;
        }

        public async Task<int?> Save(Factura oFactura)
        {
            string factura = JsonConvert.SerializeObject(oFactura, Formatting.Indented);
            int nro = await ClientSingleton.GetInstance().PostAsync("api/Facturas/PostFacturas", factura);
            return nro;
        }
    }
}
