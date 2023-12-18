using System;
using Backend.Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    interface IServicio
    {
        Task<List<Producto>> GetProductos();
        Task<int?> Save(Factura oFactura);
    }
}
