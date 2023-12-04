using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Exceptions
{
    public class ContactgegevensException : Exception
    {
        public ContactgegevensException(string? message) : base(message)
        {
        }

        public ContactgegevensException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
