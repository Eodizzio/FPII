using Backend.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaz
{
    interface IDaoPedidos
    {
        List<Cliente> GetClientes();
        List<Pedido> GetPedidos(int id, DateTime Desde, DateTime Hasta);
        int UpdatePedidos(int id, int opcion);
    }
}
