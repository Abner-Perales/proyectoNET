using ExamenPractica.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPrueba.DataAccess.Productos
{
    public class ProductosDB
    {
        private ConexionDB conexion = new ConexionDB();
        private SqlDataReader leer;
        private DataTable tabla = new DataTable();
        private SqlCommand comando = new SqlCommand();
        

        public DataTable MostrarTodo()
        {
            tabla = new DataTable();
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_MostrarProductos";
                comando.CommandType = CommandType.StoredProcedure;
                leer = comando.ExecuteReader();
                tabla.Load(leer);
            }
            catch (Exception e)
            {
                tabla = new DataTable();
            }
            finally
            {

                conexion.CerrarConexion();
            }
            
            return tabla;
        }

        public void Insertar(Producto producto)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_AgregarProducto";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Nombre", producto.NombreProducto);
                comando.Parameters.AddWithValue("@IDProveedor", producto.IDProveedor);
                comando.Parameters.AddWithValue("@IDTipo", producto.IDTipo);
                comando.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                comando.Parameters.AddWithValue("@Modelo", producto.Modelo);
                comando.Parameters.AddWithValue("@Marca", producto.Marca);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
        }

        public void EditarCantidad(int IdProducto, int Cantidad)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_ActualizarCantidad";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Id", IdProducto);
                comando.Parameters.AddWithValue("@Cantidad", Cantidad);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
        }

        public void EliminarProducto(int IdProducto)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_EliminarProducto";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Id", IdProducto);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
        }

        public DataTable MostrarFiltro(DateTime FechaInicio, DateTime FechaFin, int TodosProductos)
        {
            tabla = new DataTable();
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_MostrarFiltro";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                comando.Parameters.AddWithValue("@TodosProductos", TodosProductos);
                leer = comando.ExecuteReader();
                tabla.Load(leer);
            }
            catch (Exception e)
            {
                tabla = new DataTable();
            }
            finally
            {
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }

            return tabla;
        }
    }
}
