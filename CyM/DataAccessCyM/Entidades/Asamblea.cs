using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessCyM.Dtos;

namespace DataAccessCyM.Entidades
{
    public class Asamblea
    {
        public int IdAsamblea { get; set; }
        public Entidad Entidad { get; set; }
        public TipoAsamblea TipoAsamblea { get; set; }
        public string Ejercicio { get; set; }
        public DateTime? FechaPreAsamblea { get; set; }
        public DateTime? FechaAsamblea { get; set; }
        public DateTime? FechaPosAsamblea { get; set; }
        public DateTime? FechaTerceraPresentacion { get; set; }
        public string Observacion { get; set; }
        public string Lugar { get; set; }
        public DateTime? FechaConvocatoria { get; set; }
        public string Inspector { get; set; }
        public string Hora { get; set; }
        public bool SolicitoVeedor { get; set; }
        public List<DocumentoDto> DocumentosPreAsamblea { get; set; }
        public List<DocumentoDto> DocumentosPosAsamblea { get; set; }
        public List<Tema> Temario { get; set; }
        public List<Veedor> Veedores { get; set; }
         


    }
}
