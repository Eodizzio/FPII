using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int CodigoPostal { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public Cliente()
        {
            IdCliente = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Dni = 0;
            CodigoPostal = 0;
            Pedidos = new List<Pedido>();
        }

        public void AddPedido(Pedido oPedido)
        {
            if (oPedido != null)
                Pedidos.Add(oPedido);
        }

        public void RemovePedido(int index)
        {
            Pedidos.RemoveAt(index);
        }

        public override string ToString()
        {
            return Apellido + ", " + Nombre;
        }
    }
}
