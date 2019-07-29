using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessCyM.Dtos
{
    public class TramiteSUACDto
    {
        public string ID_TRAMITE { get; set; }
        public string NRO_TRAMITE{ get; set; }
        public string NRO_STICKER	{ get; set; }
        public string ASUNTO	{ get; set; }
        public string NOMBRE_INICIADOR	{ get; set; }
        public string NRO_DOCUMENTO	{ get; set; }
        public string FECHA_CREACION	{ get; set; }
        public string TIPO	{ get; set; }
        public string SUBTIPO	{ get; set; }
        public string UNIDAD_ACTUAL	{ get; set; }
        public string UNIDAD_PROXIMA	{ get; set; }
        public string FECHA_UNIDAD	{ get; set; }
        public string N_ESTADO	{ get; set; }
        public string ID_TRA_RELACIONADOS{ get; set; }

    }
}
