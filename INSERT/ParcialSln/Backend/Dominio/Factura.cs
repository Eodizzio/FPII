using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class Factura
    {
        public int Nro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public FormaPago formaPago { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaBaja { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Nro = 0;
            Fecha = DateTime.Now;
            Cliente = string.Empty;
            formaPago = new FormaPago();
            Total = 0;
            FechaBaja = DateTime.Now;
            Detalles = new List<DetalleFactura>();
        }

        public void AddDetalle(DetalleFactura detalle)
        {
            if (detalle != null)
                Detalles.Add(detalle);
        }

        public void RemoveDetalle(int index)
        {
            Detalles.RemoveAt(index);
        }
    }
}
