using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseLibrary.Entities;
using WarehouseLibrary.Exceptions;
using WarehouseLibrary.Services;

namespace WarehouseLibrary.Tests
{
    [TestClass]
    public class WarehouseServiceTests
    {
        [TestMethod]
        public void AddProduct_ValidProduct_AddsSuccessfully()
        {
            // Arrange
            WarehouseService service = new WarehouseService();
            Product product = new Product
            {
                Id = 1,
                Name = "Молоко",
                Price = 80m,
                Quantity = 10
            };

            // Act
            service.AddProduct(product);

            // Assert
            Product stored = service.GetProduct(1);
            Assert.AreEqual("Молоко", stored.Name);
            Assert.AreEqual(80m, stored.Price);
            Assert.AreEqual(10, stored.Quantity);
        }

        [TestMethod]
        public void AddProduct_NegativePrice_ThrowsInvalidProductParameterException()
        {
            // Arrange
            WarehouseService service = new WarehouseService();
            Product product = new Product
            {
                Id = 1,
                Name = "Хлеб",
                Price = -5m,
                Quantity = 1
            };

            // Act + Assert
            Assert.ThrowsException<InvalidProductParameterException>(
                () => service.AddProduct(product));
        }

        [TestMethod]
        public void AddProduct_EmptyName_ThrowsInvalidProductParameterException()
        {
            WarehouseService service = new WarehouseService();
            Product product = new Product
            {
                Id = 1,
                Name = "",
                Price = 50m,
                Quantity = 1
            };

            Assert.ThrowsException<InvalidProductParameterException>(
                () => service.AddProduct(product));
        }

        [TestMethod]
        public void AddProduct_DuplicateId_ThrowsInvalidProductParameterException()
        {
            WarehouseService service = new WarehouseService();
            service.AddProduct(new Product { Id = 1, Name = "Молоко", Price = 80m, Quantity = 5 });

            Assert.ThrowsException<InvalidProductParameterException>(
                () => service.AddProduct(
                    new Product { Id = 1, Name = "Кефир", Price = 90m, Quantity = 3 }));
        }

        [TestMethod]
        public void GetProduct_MissingId_ThrowsProductNotFoundException()
        {
            // Arrange
            WarehouseService service = new WarehouseService();

            // Act
            ProductNotFoundException ex =
                Assert.ThrowsException<ProductNotFoundException>(
                    () => service.GetProduct(42));

            // Assert
            Assert.AreEqual(42, ex.ProductId);
        }

        [TestMethod]
        public void RemoveProduct_ExistingId_RemovesSuccessfully()
        {
            WarehouseService service = new WarehouseService();
            service.AddProduct(new Product { Id = 1, Name = "Молоко", Price = 80m, Quantity = 5 });

            service.RemoveProduct(1);

            Assert.ThrowsException<ProductNotFoundException>(() => service.GetProduct(1));
        }

        [TestMethod]
        public void ShipProduct_NotEnoughStock_ThrowsInsufficientStockException()
        {
            // Arrange
            WarehouseService service = new WarehouseService();
            service.AddProduct(new Product { Id = 1, Name = "Сахар", Price = 100m, Quantity = 3 });

            // Act
            InsufficientStockException ex =
                Assert.ThrowsException<InsufficientStockException>(
                    () => service.ShipProduct(1, 5));

            // Assert
            Assert.AreEqual("Сахар", ex.ProductName);
            Assert.AreEqual(3, ex.Available);
            Assert.AreEqual(5, ex.Requested);
        }

        [TestMethod]
        public void ShipProduct_EnoughStock_DecreasesQuantity()
        {
            WarehouseService service = new WarehouseService();
            service.AddProduct(new Product { Id = 1, Name = "Сахар", Price = 100m, Quantity = 10 });

            service.ShipProduct(1, 4);

            Product stored = service.GetProduct(1);
            Assert.AreEqual(6, stored.Quantity);
        }

        [TestMethod]
        public void ShipProduct_ZeroCount_ThrowsInvalidProductParameterException()
        {
            WarehouseService service = new WarehouseService();
            service.AddProduct(new Product { Id = 1, Name = "Сахар", Price = 100m, Quantity = 10 });

            Assert.ThrowsException<InvalidProductParameterException>(
                () => service.ShipProduct(1, 0));
        }
    }
}