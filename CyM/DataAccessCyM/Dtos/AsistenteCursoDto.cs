using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessCyM.Entidades;

namespace DataAccessCyM.Dtos
{
    public class AsistenteCursoDto
    {
      public int NroOrden { get; set; }
      public string NombreCompletoPersona { get; set; }
      
      public string Sexo { get; set; }
      public string Dni { get; set; }
      public string idSexo { get; set; }
      public string idNumero { get; set; }
      public string PaiCodPais { get; set; }
      public Comunicacion Contacto { get; set; }
    }
}