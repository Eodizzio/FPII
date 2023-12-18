using Backend.Dominio;
using Backend.Interfaz;
using Backend.Singleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Implementación
{
    public class DaoPedidos : IDaoPedidos
    {
        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            DataTable tabla = DBHelper.GetInstance().ConsultarSql("SP_CONSULTAR_CLIENTES");
            foreach(DataRow fila in tabla.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.IdCliente = (int)fila["id_cliente"];
                cliente.Nombre = fila["nombre"].ToString();
                cliente.Apellido = fila["apellido"].ToString();
                cliente.Dni = (int)fila["dni"];
                cliente.CodigoPostal = (int)fila["cod_postal"];
                clientes.Add(cliente);
            }
            return clientes;
        }

        public List<Pedido> GetPedidos(int id, DateTime Desde, DateTime Hasta)
        {
            List<Pedido> pedidos = new List<Pedido>();
            List<SqlParameter> listaParam = new List<SqlParameter>();
            listaParam.Add(new SqlParameter("@cliente", id));
            listaParam.Add(new SqlParameter("@fecha_desde", Desde));
            listaParam.Add(new SqlParameter("@fecha_hasta", Hasta));
            DataTable tabla = DBHelper.GetInstance().ConsultarSql("SP_CONSULTAR_PEDIDOS", listaParam);
            foreach(DataRow fila in tabla.Rows)
            {
                Pedido pedido = new Pedido();
                pedido.Codigo = (int)fila["codigo"];
                pedido.FechaEntrega = (DateTime)fila["fec_entrega"];
                pedido.DireccionEntrega = fila["dir_entrega"].ToString();
                pedido.Entregado = fila["entregado"].ToString();
                pedido.Cliente = fila["cliente"].ToString();
                pedidos.Add(pedido);
            }
            return pedidos;
        }

        public int UpdatePedidos(int id, int opcion)
        {
            int afectadas;
            string sp;
            if (opcion == 1)
                sp = "SP_REGISTRAR_ENTREGA";
            else
                sp = "SP_REGISTRAR_BAJA";
            List<SqlParameter> listaParam = new List<SqlParameter>();
            listaParam.Add(new SqlParameter("@codigo", id));
            afectadas = DBHelper.GetInstance().EjecutarSql(sp, listaParam);
            return afectadas;
        }
    }
}
