using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPrueba.DataAccess
{
    public class ConexionDB
    {
        string con = "Data Source=DESKTOP-Q327VAB\\SQLEXPRESS;Initial Catalog=Examen;User ID=abner;Password=abner";
        private SqlConnection conexion;

        public ConexionDB()
        {
            conexion = new SqlConnection(con);
        }

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }

            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }

            return conexion;
        }
    }
}
