using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class DetalleFactura
    {
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }

        public DetalleFactura()
        {
            oProducto = new Producto();
            Cantidad = 0;
        }
    }
}
