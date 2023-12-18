using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Activo { get; set; }

        public Producto()
        {
            idProducto = 0;
            Nombre = string.Empty;
            Precio = 0;
            Activo = string.Empty;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
