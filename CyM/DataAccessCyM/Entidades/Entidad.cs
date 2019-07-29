using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Entidades
{
    public class Entidad
    {
        
        public int id_entidad { get; set; } 
        public string id_tipo_entidad { get; set; } 
        public string nro_rp { get; set; } 
        public string nro_matricula { get; set; } 
        public string descripcion { get; set; } 
        public string cuit { get; set; } 
        public int? id_estado { get; set; } 
        public int? id_dom_legal { get; set; } //id_vin de dom_manager
        public string resolucion_corte { get; set; } 
        public DateTime? resolucion_fecha { get; set; } 
        public string resolucion_nro { get; set; } 
        public string resolucion_tipo { get; set; } 
        public int? rg_folio { get; set; } 
        public string rg_nro { get; set; } 
        public string rg_volumen { get; set; } 
        public DateTime? estatuto_fec_aprobacion { get; set; } 
        public DateTime? estatuto_fec_reforma { get; set; } 
        public DateTime? fec_cierre_ejercicio { get; set; }
        public string filial { get; set; }
        public string observacion  { get; set; }
        public string NRO_SOLICITUD_CURSO  { get; set; }
        public string NRO_EXPEDIENTE_SAUC  { get; set; }
        public DateTime? fecha_elevacion { get; set; }
        public DateTime? FECHA_APROBACION  { get; set; }
        public Domicilio Domicilio  { get; set; }
        public Comunicacion Contacto  { get; set; }
        public List<TramiteSUACDto> ExpedientesSuac  { get; set; }

        public List<HistorialEstadoEntidad> HistorialEstado { get; set; }

        /// <summary>
        /// Cuil del usuario logueado que se utiiliza para guardar el cambio de estado en el historial.
        /// </summary>
        public string CUIL_USR_CIDI { get; set; }


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

        /// <summary>
        /// Devuelve el ID_TIPO_ENTIDAD de una Mutual de BD, que está guardado en la vista T_COMUNES.T_FORMA_JURIDICA
        /// </summary>
        public static string TipoEntidadAsociacionCivil
        {
            get { return "33"; }
        }

        /// <summary>
        /// Devuelve el ID_TIPO_ENTIDAD de una Mutual de BD, que está guardado en la vista T_COMUNES.T_FORMA_JURIDICA
        /// </summary>
        public static string TipoEntidadFundacion
        {
            get { return "24"; }
        }
    }
}
