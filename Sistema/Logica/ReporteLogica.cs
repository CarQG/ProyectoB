using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Logica
{
    public class ReporteLogica
    {

        private static ReporteLogica _instancia = null;

        public ReporteLogica()
        {

        }

        public static ReporteLogica Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ReporteLogica();

                return _instancia;
            }
        }

        public DataTable consulta(string fechainicio,string fechafin,string tipopermiso) {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select r.UsuarioCreacion[Usuario Creacion], strftime('%d/%m/%Y', date(r.FechaCreacion))[Fecha Creacion],r.TipoPermiso[Tipo Permiso],");
                    sb.AppendLine("strftime('%d/%m/%Y', date( r.Fecha))[Fecha Servicio],r.Descripcion_en[Descripcion_en],r.Comentario,");
                    sb.AppendLine("ifnull(p.Documento,'')[Documento Cliente],ifnull(p.Nombres,r.NombreCliente)[Nombre Cliente],");
                    sb.AppendLine("r.Cantidad ");
                    sb.AppendLine("from Registros r");
                    sb.AppendLine("left join Persona p on p.IdPersona = r.IdPersona");
                    sb.AppendLine("WHERE DATE(r.Fecha) BETWEEN @fechainicio AND @fechafin");
                    sb.AppendLine("AND r.TipoPermiso = iif(@param_tipopermiso = '0',r.TipoPermiso,@param_tipopermiso)");
            //      sb.AppendLine("AND r.TipoMoneda = iif(@tipomoneda = '0',r.TipoMoneda,@tipomoneda)");
                    sb.AppendLine("order by r.FechaCreacion asc");

                    SQLiteDataAdapter da = new SQLiteDataAdapter(sb.ToString(), conexion);
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@fechainicio",fechainicio));
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@fechafin", fechafin));
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@param_tipopermiso", tipopermiso));
                  //da.SelectCommand.Parameters.Add(new SQLiteParameter("@tipomoneda", tipomoneda));
                    da.Fill(dt);
                    
                }

            }
            catch (Exception ex) {
                dt = new DataTable();
            }

            return dt;
        }

        public DataTable contable(string fechainicio,string fechafin,string tipopermiso) {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select strftime('%d/%m/%Y', Fecha)[Fecha],Descripcion_en,Cantidad from Registros");
                    sb.AppendLine("where TipoPermiso = @p_tipopermiso");
                    sb.AppendLine("select Descripcion from Servicios");
                    sb.AppendLine("and date(Fecha)  BETWEEN @fechainicio AND @fechafin");

                    SQLiteDataAdapter da = new SQLiteDataAdapter(sb.ToString(), conexion);
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@fechainicio",fechainicio));
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@fechafin", fechafin));
                    da.SelectCommand.Parameters.Add(new SQLiteParameter("@p_tipopermiso", tipopermiso));
                  // da.SelectCommand.Parameters.Add(new SQLiteParameter("@p_descripcion", descripcion));
                    //da.Fill(descripcion);
                }

            }
            catch (Exception ex) {
                //descripcion = new DataTable();
            }

            return dt;
        }

    }
}
