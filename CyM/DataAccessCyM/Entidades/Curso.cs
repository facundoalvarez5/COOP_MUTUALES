using System;
using System.Collections.Generic;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Entidades
{
    public class Curso
    {
        public int IdCurso { get; set; }
        public string n_curso { get; set; }
        public DateTime? FechaDictado { get; set; }
        public LocalidadDto Localidad { get; set; } 
        public string CantidadAsistentes { get; set; } 
        public List<SolicitudCursoDto> Soliciudes { get; set; }
        public string Descripcion { get; set; } 
    }
}
