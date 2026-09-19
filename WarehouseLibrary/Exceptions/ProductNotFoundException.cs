using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseLibrary.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public int ProductId { get; private set; }

        public ProductNotFoundException(int productId)
            : base("Товар с Id=" + productId + " не найден на складе.")
        {
            ProductId = productId;
        }
    }
}
