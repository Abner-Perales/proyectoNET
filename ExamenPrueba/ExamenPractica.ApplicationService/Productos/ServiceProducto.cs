using ExamenPractica.Core;
using ExamenPrueba.DataAccess.Productos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPractica.ApplicationService.Productos
{
    public class ServiceProducto
    {
        private ProductosDB productosDB = new ProductosDB();

        public DataTable MostrarTodos()
        {
            DataTable tabla = new DataTable();
            tabla = productosDB.MostrarTodo();
            return tabla;
        }

        public void Insertar(string nombre, string idProveedor, string idTipo, string cantidad, string modelo, string marca)
        {
            Producto producto = new Producto();

            producto.NombreProducto = nombre;
            producto.Modelo = modelo;
            producto.Marca = marca;

            try
            {
                producto.IDProveedor = Int32.Parse(idProveedor);
            }
            catch (Exception e)
            {
                producto.IDProveedor = 0;
            }

            try
            {
                producto.IDTipo = Int32.Parse(idTipo);
            }
            catch (Exception e)
            {
                producto.IDTipo = 0;
            }

            try
            {
                producto.Cantidad = Int32.Parse(cantidad);
            }
            catch (Exception e)
            {
                producto.Cantidad = 0;
            }

            productosDB.Insertar(producto);
        }

        public void EditarCantidad(string id, string cantidad)
        {
            int IdProducto, Cantidad;

            try
            {
               IdProducto = Int32.Parse(id);
            }
            catch (Exception e)
            {
                IdProducto = 0;
            }

            try
            {
                Cantidad = Int32.Parse(cantidad);
            }
            catch (Exception e)
            {
                Cantidad = 0;
            }

            productosDB.EditarCantidad(IdProducto, Cantidad);
        }

        public void EliminarProducto(string id)
        {
            int IdProducto;
            try
            {
                IdProducto = Int32.Parse(id);
            }
            catch (Exception e)
            {
                IdProducto = 0;
            }

            productosDB.EliminarProducto(IdProducto);
        }

        public DataTable MostrarFiltro(DateTime FechaInicio, DateTime FechaFin, int TodosProducto)
        {
            DataTable tabla = new DataTable();
            tabla = productosDB.MostrarFiltro(FechaInicio, FechaFin, TodosProducto);
            return tabla;
        }


    }
}
