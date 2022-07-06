using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPractica.Core
{
    public class Producto
    {
        public string NombreProducto { get; set; }
        public int IDProveedor { get; set; }
        public int IDTipo { get; set; }
        public int Cantidad { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
    }
}
