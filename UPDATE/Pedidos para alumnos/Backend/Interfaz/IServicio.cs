using Backend.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaz
{
    interface IServicio
    {
        Task<List<Cliente>> GetClientes();
        Task<List<Pedido>> GetPedidos(int id, DateTime Desde, DateTime Hasta);
        Task<string> UpdatePedidos(int id, int opcion);
    }
}
