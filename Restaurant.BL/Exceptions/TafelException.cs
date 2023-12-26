using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Exceptions
{
    public class TafelException : Exception
    {
        public TafelException(string? message) : base(message)
        {
        }

        public TafelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
