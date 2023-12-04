using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatalayerProvider.Exceptions
{
    public class EFDatalayerFactoryException : Exception
    {
        public EFDatalayerFactoryException(string? message) : base(message)
        {
        }

        public EFDatalayerFactoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
