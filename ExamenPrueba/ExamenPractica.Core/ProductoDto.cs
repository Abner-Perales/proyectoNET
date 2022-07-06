using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPractica.Core
{
    public class ProductoDto
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Proveedor { get; set; }
        public string TipoProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAlta { get; set; }
        
    }
}
