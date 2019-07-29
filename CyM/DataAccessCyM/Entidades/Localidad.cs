using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessCyM.Entidades
{
    public class Localidad
    {
        public string IdLocalidad { get; set; }
        public string NombreLocalidad { get; set; }
        public string IdDepartamento { get; set; }
        public string IdProvincia { get; set; } 
    }
}
