using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo
{
    public class Registro
    {
        public int IdRegistro { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public int IdProveedor { get; set; }
        public string NombreCliente { get; set; }
        public int Id_L { get; set; }
        public string Descripcion_en { get; set; }
        public string Comentario { get; set; }
        public string Fecha { get; set; }
        public string Cantidad { get; set; }
        public string TipoRegistro { get; set; }
    }
}
