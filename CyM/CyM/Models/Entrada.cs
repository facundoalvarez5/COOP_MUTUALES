using System;

namespace CyM.Models
{
    public class Entrada
    {
        public int IdAplicacion { get; set; }
        public String Contrasenia { get; set; }
        public String HashCookie { get; set; }
        public String TokenValue { get; set; }
        public String TimeStamp { get; set; }
        public String CUIL { get; set; }
    }

    public class EntradaLogin : Entrada
    {
        public String ContraseniaUsuario { get; set; }
    }
}