using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessCyM.Entidades
{
    public class Comunicacion
    {
        public String IdEntidad { get; set; }
        
        public String EMail { get; set; }
        public String NroCel { get; set; }
        public String CodAreaCel { get; set; }
        public String NroTelfijo { get; set; }
        public String CodAreaTelFijo { get; set; }
        public string Tabla_Origen { get; set; }
 
        
        
    }
}
