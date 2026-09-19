using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseLibrary.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format("Товар #{0}: {1}, цена = {2:C}, количество = {3}",
                Id, Name, Price, Quantity);
        }
    }
}
