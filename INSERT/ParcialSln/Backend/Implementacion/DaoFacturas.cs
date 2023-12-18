using Backend.Interfaces;
using Backend.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Backend.Singleton;
using System.Data.SqlClient;

namespace Backend.Implementacion
{
    public class DaoFacturas : IDaoFacturas
    {
        public List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            DataTable tabla = DBHelper.GetInstance().ConsultarSql("SP_CONSULTAR_PRODUCTOS");
            foreach(DataRow fila in tabla.Rows)
            {
                Producto producto = new Producto();
                producto.idProducto = (int)fila["id_producto"];
                producto.Nombre = fila["n_producto"].ToString();
                producto.Precio = (decimal)fila["precio"];
                producto.Activo = fila["activo"].ToString();
                productos.Add(producto);
            }
            return productos;
        }

        public int Save(Factura oFactura)
        {
            int afectadas;
            SqlParameter salida = new SqlParameter("@nro", SqlDbType.Int);
            salida.Direction = ParameterDirection.Output;
            List<SqlParameter> listaParam = new List<SqlParameter>();
            listaParam.Add(new SqlParameter("@cliente", oFactura.Cliente));
            listaParam.Add(new SqlParameter("@forma", oFactura.formaPago.Id));
            listaParam.Add(new SqlParameter("@total", oFactura.Total));
            int nroFactura = DBHelper.GetInstance().EjecutarSql("SP_INSERTAR_FACTURA", listaParam, salida);
            int nroOrden = 0;
            foreach(DetalleFactura detalle in oFactura.Detalles)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@nro", nroFactura));
                param.Add(new SqlParameter("@detalle", ++nroOrden));
                param.Add(new SqlParameter("@id_producto", detalle.oProducto.idProducto));
                param.Add(new SqlParameter("@cantidad", detalle.Cantidad));
                afectadas = DBHelper.GetInstance().EjecutarSql("SP_INSERTAR_DETALLES", param);
            }
            return nroFactura;
        }
    }
}
