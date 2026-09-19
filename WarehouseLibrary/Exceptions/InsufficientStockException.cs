using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseLibrary.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public string ProductName { get; private set; }
        public int Available { get; private set; }
        public int Requested { get; private set; }

        public InsufficientStockException(string productName, int available, int requested)
            : base("Недостаточно товара «" + productName + "»: доступно " +
                   available + ", запрошено " + requested + ".")
        {
            ProductName = productName;
            Available = available;
            Requested = requested;
        }
    }
}
