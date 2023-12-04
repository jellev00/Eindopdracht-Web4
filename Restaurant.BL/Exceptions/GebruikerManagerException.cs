using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Exceptions
{
    public class GebruikerManagerException : Exception
    {
        public GebruikerManagerException(string? message) : base(message)
        {
        }

        public GebruikerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
