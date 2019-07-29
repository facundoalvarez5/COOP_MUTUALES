using System;

namespace CyM.Models
{
    public class Respuesta
    {
        public String Resultado { get; set; }
        public String CodigoError { get; set; }
        public String ExisteUsuario { get; set; }
        public String SesionHash { get; set; }
    }
}