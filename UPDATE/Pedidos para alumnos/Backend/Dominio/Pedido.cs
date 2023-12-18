using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class Pedido
    {
        public int Codigo { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string Entregado { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Cliente { get; set; }

        public Pedido()
        {
            Codigo = 0;
            FechaEntrega = DateTime.Now;
            DireccionEntrega = string.Empty;
            Entregado = string.Empty;
            FechaBaja = DateTime.Now;
            Cliente = string.Empty;
        } 
    }
}
