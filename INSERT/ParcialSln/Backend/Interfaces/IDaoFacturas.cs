using System;
using Backend.Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    interface IDaoFacturas
    {
        List<Producto> GetProductos();
        int Save(Factura oFactura);
    }
}
