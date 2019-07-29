    using System;
using System.Configuration;

namespace DataAccessCyM
{
    public abstract class daBase
    {
        protected String CadenaDeConexion()
        {
            //cadena en DESARROLLO
            return ConfigurationManager.ConnectionStrings["CADENA_DE_CONEXION"].ConnectionString;    
        }
    }
}
