using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    public class Pedido
    {
        public string Codigo { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime direccionEntrega { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Entregado { get; set; }
    }
}
