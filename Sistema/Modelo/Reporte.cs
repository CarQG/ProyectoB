using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo
{
    public class Reporte
    {
        
        public string FechaCreacion { get; set; }
        public string TipoPermiso { get; set; }
        public string Fecha { get; set; }
        public string Descripcion_en { get; set; }
        public string Comentario { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Cantidad { get; set; }
    }
}
