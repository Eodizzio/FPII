using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Singleton
{
    public class DBHelper
    {
        private static DBHelper instance;
        private SqlConnection cnn;
        private SqlCommand cmd;
        private string cnnString = @"Data Source=DESKTOP-R1N4D40\SQLEXPRESS;Initial Catalog=db_facturas;Integrated Security=True";

        public DBHelper()
        {
            cnn = new SqlConnection(cnnString);
        }

        public static DBHelper GetInstance()
        {
            if(instance == null)
                instance = new DBHelper();
            return instance;
        }

        public DataTable ConsultarSql(string spNombre)
        {
            DataTable tabla = new DataTable();
            cnn.Open();
            cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();
            return tabla;
        }

        public int EjecutarSql(string spNombre, List<SqlParameter> listaParam = null, SqlParameter salida = null)
        {
            int afectadas = 0;
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd = new SqlCommand(spNombre, cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                if (salida != null)
                    cmd.Parameters.Add(salida);
                if(listaParam != null)
                {
                    foreach (SqlParameter param in listaParam)
                        cmd.Parameters.Add(param);
                }
                if (salida != null)
                {
                    cmd.ExecuteNonQuery();
                    afectadas = (int)salida.Value;
                }
                else
                    afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
            return afectadas;
        }
    }
}
