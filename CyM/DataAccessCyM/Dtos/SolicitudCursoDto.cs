using System.Collections.Generic;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Dtos
{
    public class SolicitudCursoDto
    {
        public string NombreCooperativa { get; set; }
        public string NombreDepartamento { get; set; }
        public string NombreLocalidad { get; set; }
        public string idLocalidad { get; set; } 
        public string NroSolicitudCurso { get; set; } 
        public string FechaAlta { get; set; }
        public LocalidadDto LocalidadDto { get; set; }
        public string asignado { get; set; }
        public string CuilUsuarioCidi { get; set; }
        public string Estado { get; set; }
        public string LinkArchivosAsistentes { get; set; } 

        public List<AsistenteCursoDto> SolicitantesCurso  { get; set; }
        public int CantidadEstimada { get; set; }
    }
}
