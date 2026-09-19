using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseLibrary.Exceptions
{
    public class InvalidProductParameterException : Exception
    {
        public InvalidProductParameterException(string message)
            : base(message)
        {
        }

        public InvalidProductParameterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
