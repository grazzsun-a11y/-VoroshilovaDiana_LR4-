using System;
using WarehouseLibrary.Entities;
using WarehouseLibrary.Exceptions;
using WarehouseLibrary.Services;

namespace WarehouseDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            WarehouseService service = new WarehouseService();

            Console.WriteLine("=== ПРАКТИЧЕСКАЯ ПРОВЕРКА ИСКЛЮЧЕНИЙ ===\n");

            // --- 1. Успешное добавление ---
            Console.WriteLine("1. Добавление корректных товаров:");
            try
            {
                service.AddProduct(new Product { Id = 1, Name = "Молоко", Price = 80m, Quantity = 10 });
                service.AddProduct(new Product { Id = 2, Name = "Хлеб", Price = 40m, Quantity = 5 });
                Console.WriteLine("   OK: товары добавлены.");
            }
            catch (InvalidProductParameterException ex)
            {
                Console.WriteLine("   [Ошибка] " + ex.Message);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 2. Отрицательная цена ---
            Console.WriteLine("2. Товар с отрицательной ценой:");
            try
            {
                service.AddProduct(new Product { Id = 3, Name = "Сыр", Price = -100m, Quantity = 1 });
                Console.WriteLine("   Товар добавлен (этого быть не должно).");
            }
            catch (InvalidProductParameterException ex)
            {
                Console.WriteLine("   [ПОЙМАНО ИСКЛЮЧЕНИЕ] " + ex.Message);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 3. Пустое имя ---
            Console.WriteLine("3. Товар с пустым именем:");
            try
            {
                service.AddProduct(new Product { Id = 4, Name = "", Price = 50m, Quantity = 1 });
                Console.WriteLine("   Товар добавлен (этого быть не должно).");
            }
            catch (InvalidProductParameterException ex)
            {
                Console.WriteLine("   [ПОЙМАНО ИСКЛЮЧЕНИЕ] " + ex.Message);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 4. Несуществующий товар ---
            Console.WriteLine("4. Поиск несуществующего товара:");
            try
            {
                Product p = service.GetProduct(999);
                Console.WriteLine("   Найден: " + p);
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine("   [ПОЙМАНО ИСКЛЮЧЕНИЕ] " + ex.Message);
                Console.WriteLine("   Id искомого товара: " + ex.ProductId);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 5. Отгрузка больше, чем есть ---
            Console.WriteLine("5. Отгрузка большего количества, чем есть на складе:");
            try
            {
                service.ShipProduct(2, 100);
                Console.WriteLine("   Отгружено (этого быть не должно).");
            }
            catch (InsufficientStockException ex)
            {
                Console.WriteLine("   [ПОЙМАНО ИСКЛЮЧЕНИЕ] " + ex.Message);
                Console.WriteLine("   Товар: " + ex.ProductName);
                Console.WriteLine("   Доступно: " + ex.Available);
                Console.WriteLine("   Запрошено: " + ex.Requested);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 6. Дублирование Id ---
            Console.WriteLine("6. Добавление товара с уже существующим Id:");
            try
            {
                service.AddProduct(new Product { Id = 1, Name = "Кефир", Price = 90m, Quantity = 3 });
                Console.WriteLine("   Товар добавлен (этого быть не должно).");
            }
            catch (InvalidProductParameterException ex)
            {
                Console.WriteLine("   [ПОЙМАНО ИСКЛЮЧЕНИЕ] " + ex.Message);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- 7. Успешная отгрузка после всех ошибок ---
            Console.WriteLine("7. Корректная отгрузка (программа продолжает работу):");
            try
            {
                service.ShipProduct(1, 3);
                Console.WriteLine("   OK: отгружено 3 единицы товара #1.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("   [Ошибка] " + ex.Message);
            }
            finally
            {
                Console.WriteLine("   --- Операция завершена ---\n");
            }

            // --- Итоговое состояние ---
            Console.WriteLine("=== Состояние склада после всех операций ===");
            foreach (Product p in service.GetAllProducts())
            {
                Console.WriteLine("  " + p);
            }

            Console.WriteLine("\nПрограмма отработала без аварийного завершения.");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}