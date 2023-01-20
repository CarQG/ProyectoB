using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo
{
    public class Persona
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
       
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string TipoPermiso { get; set; }
    }
}
