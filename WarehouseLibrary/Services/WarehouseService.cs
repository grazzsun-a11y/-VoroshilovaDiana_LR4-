using System;
using System.Collections.Generic;
using System.Text;
using WarehouseLibrary.Entities;
using WarehouseLibrary.Exceptions;
using WarehouseLibrary.Interfaces;

namespace WarehouseLibrary.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly Dictionary<int, Product> _products =
            new Dictionary<int, Product>();

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new InvalidProductParameterException("Товар не может быть null.");

            if (product.Id <= 0)
                throw new InvalidProductParameterException("Id товара должен быть положительным.");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new InvalidProductParameterException("Название товара не может быть пустым.");

            if (product.Price < 0)
                throw new InvalidProductParameterException("Цена не может быть отрицательной.");

            if (product.Quantity < 0)
                throw new InvalidProductParameterException("Количество не может быть отрицательным.");

            if (_products.ContainsKey(product.Id))
                throw new InvalidProductParameterException(
                    "Товар с Id=" + product.Id + " уже существует.");

            _products.Add(product.Id, product);
        }

        public void RemoveProduct(int id)
        {
            if (!_products.ContainsKey(id))
                throw new ProductNotFoundException(id);

            _products.Remove(id);
        }

        public Product GetProduct(int id)
        {
            Product product;
            if (!_products.TryGetValue(id, out product))
                throw new ProductNotFoundException(id);

            return product;
        }

        public void ShipProduct(int id, int count)
        {
            if (count <= 0)
                throw new InvalidProductParameterException(
                    "Количество отгрузки должно быть положительным.");

            Product product = GetProduct(id);

            if (product.Quantity < count)
                throw new InsufficientStockException(product.Name, product.Quantity, count);

            product.Quantity -= count;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.Values;
        }
    }
}
