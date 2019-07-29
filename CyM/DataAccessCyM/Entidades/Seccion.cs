using System;
using System.Collections.Generic;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Entidades
{
    public class Seccion
    {
        public string IdSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public string IdTipoEntidad { get; set; }
        public string NombreTipoEntidad { get; set; }
        public string Motivo { get; set; }
    }
}
