using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessCyM.Entidades
{
    public class HistorialEstadoEntidad
    {
        
        public int IdHistorialEstado { get; set; } 
        public string IdEstado  { get; set; }
        public string NombreEstado  { get; set; }
        public DateTime? FechaDesde  { get; set; }
        public DateTime? FechaHasta  { get; set; }
        public string Descripcion  { get; set; }
        public string CuilUsuarioCidi  { get; set; }


        /// <summary>
        /// Devuelve el ID_TIPO_ENTIDAD de una Cooperativa de BD, que está guardado en la vista T_COMUNES.T_FORMA_JURIDICA
        /// </summary>
        public static string TipoEntidadCooperativa
        {
            get { return "16"; }
        }


        /// <summary>
        /// Devuelve el ID_TIPO_ENTIDAD de una Mutual de BD, que está guardado en la vista T_COMUNES.T_FORMA_JURIDICA
        /// </summary>
        public static string TipoEntidadMutual
        {
            get { return "26"; }
        }

        public string IdEntidad { get; set; }
    }
}
