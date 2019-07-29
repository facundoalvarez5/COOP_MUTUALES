using System;

namespace DataAccessCyM
{
    public class daException : Exception
    {
        public daException(String mensaje)
            : base(mensaje)
        {
        }
    }
}
