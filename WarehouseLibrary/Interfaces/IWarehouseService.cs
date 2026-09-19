using System;
using System.Collections.Generic;
using System.Text;
using WarehouseLibrary.Entities;

namespace WarehouseLibrary.Interfaces
{
    public interface IWarehouseService
    {
        void AddProduct(Product product);
        void RemoveProduct(int id);
        Product GetProduct(int id);
        void ShipProduct(int id, int count);
        IEnumerable<Product> GetAllProducts();
    }
}
