using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessCyM.Entidades;

namespace DataAccessCyM.Dtos
{
    public class CooperativaDto : Entidad
    {
        
        public EstadoEntidad EstadoEntidad { get; set; }
        public DateTime? Fecha_Ult_Estado { get; set; }

        //public int? id_dom_legal { get; set; } //id_vin de dom_manager
    }
}