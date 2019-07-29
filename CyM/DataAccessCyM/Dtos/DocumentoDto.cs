using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessCyM.Dtos
{
    public class DocumentoDto
    {

        public int ID_DOCUMENTO { get; set; }
        public string N_DOCUMENTO { get; set; }
        public string OBLIGATORIO { get; set; }
        public string ID_TIPO_ENTIDAD { get; set; }
        public string N_FORMA_JURIDICA { get; set; }
        public string ID_TIPO_ASAMBLEA { get; set; }
        public string N_TIPO_ASAMBLEA { get; set; }
        public string N_PRESENTACION { get; set; }
        public string CANT_DIAS { get; set; }
        public string INSTANCIA { get; set; }
        public bool Presentada { get; set; }


    }
}
