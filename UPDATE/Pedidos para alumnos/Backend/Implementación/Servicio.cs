using Backend.Dominio;
using Backend.Interfaz;
using Backend.Singleton;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Implementación
{
    public class Servicio : IServicio
    {
        public async Task<List<Cliente>> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            string content = await ClientSingleton.GetInstance().GetAsync("api/Pedidos/GetClientes");
            if (content != string.Empty)
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);
            return clientes;
        }

        public async Task<List<Pedido>> GetPedidos(int id, DateTime Desde, DateTime Hasta)
        {
            string desde = Desde.ToString("yyyy-MM-ddTHH:mm:ss");
            string hasta = Hasta.ToString("yyyy-MM-ddTHH:mm:ss");
            List<Pedido> pedidos = new List<Pedido>();
            string content = await ClientSingleton.GetInstance().GetAsync("api/Pedidos/GetPedidos/"+id+"/"+desde+"/"+hasta);
            if (content != string.Empty)
                pedidos = JsonConvert.DeserializeObject<List<Pedido>>(content);
            return pedidos;
        }

        public async Task<string> UpdatePedidos(int id, int opcion)
        {
            string content = await ClientSingleton.GetInstance().PutAsync("api/Pedidos/PutPedidos/"+id+"/"+opcion);
            return content;
        }
    }
}
