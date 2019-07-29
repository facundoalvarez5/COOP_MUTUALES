using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessCyM.Entidades
{
    public class Autoridad
    {
        public int NroOrden { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string IdSexo { get; set; }
        public string IdNumero { get; set; }
        public string PaiCodPais { get; set; }
        public Cargo Cargo { get; set; }
    }
}
