using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyM
{
    public enum EstadoAbmcEnum
    {
        CONSULTANDO = 1,
        REGISTRANDO,
        EDITANDO,
        ELIMINANDO,
        VIENDO,
        ACTIVANDO_COOPERATIVA
    }

    public enum TipoArchivoEnum
    {
        Pdf,
        Excel
    }
     
}