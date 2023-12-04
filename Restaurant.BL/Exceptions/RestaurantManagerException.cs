using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Exceptions
{
    public class RestaurantManagerException : Exception
    {
        public RestaurantManagerException(string? message) : base(message)
        {
        }

        public RestaurantManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
