using System;

namespace BlCyM
{
    public class BlException : Exception
    {
        public BlException(String mensaje)
            : base(mensaje)
        {
        }

        protected BlException()
        {
            
        }
    }
}
