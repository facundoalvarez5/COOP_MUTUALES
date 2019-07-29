using System;
using System.Collections.Generic;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Entidades
{
    public class SolicitudCurso
    {
        public DateTime? fec_solicitud { get; set; }
        public string NombreCooperativa { get; set; }
        public string IdDepartamento { get; set; }
        public string IdLocalidad { get; set; } 
        public string NroSolicitudCurso { get; set; } 
        public List<AsistenteCursoDto> SolicitantesCurso  { get; set; } 
        public string CuilUsuarioLogueado  { get; set; } 
        public string LinkArchivoAsistentes  { get; set; }
        public string cantAsistentes { get; set; }
    }
}
