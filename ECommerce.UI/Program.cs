using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Your_Connection_String_Here") // Replace with actual connection string
                .Options;

            using (AppDbContext appDbContext = new AppDbContext())
            {
                ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
                ICategoryService categoryService = new CategoryManager(categoryRepository);

                IUserRepository userRepository = new UserRepository(appDbContext);
                IUserService userService = new UserManager(userRepository);

                IProductRepository productRepository = new ProductRepository(appDbContext);
                IProductService productService = new ProductManager(productRepository);

                IOrderRepository orderRepository = new OrderRepository(appDbContext);
                IOrderService orderService = new OrderManager(orderRepository);

                Basket basket = new Basket { BasketItems = new List<BasketItem>() };

                Console.WriteLine("Welcome to E-Commerce site");
                Console.WriteLine(new string('-',50));
                Console.WriteLine("Enter user name");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter user name");
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.WriteLine(new string('-', 50));

                var user = userService.Get(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    Console.WriteLine("Invalid username or password. Exiting...");
                    return;
                }

                if (user.UserType == UserType.Admin)
                {
                    AdminMenu(categoryService, productService);
                }
                else if (user.UserType == UserType.User)
                {
                    UserMenu(user, basket, productService);
                }
            }
        }

        static void AdminMenu(ICategoryService categoryService, IProductService productService)
        {
            Console.WriteLine("Choose one of the commands:");
            Console.WriteLine("1 - Show all products");
            Console.WriteLine("2 - Add new product");

            if (int.TryParse(Console.ReadLine(), out int command))
            {
                if (command == 1)
                {
                    ShowAllProducts(productService);
                }
                else if (command == 2)
                {
                    AddNewProduct(categoryService, productService);
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void ShowAllProducts(IProductService productService)
        {
            var products = productService.GetAll(p => p.Id < 100);
            if (!products.Any())
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                foreach (var item in products)
                {
                    Console.WriteLine($"{item.Id} - {item.Name} -{item.CategoryName}- {item.Price:C}");
                }
            }
        }

        static void AddNewProduct(ICategoryService categoryService, IProductService productService)
        {
            Console.WriteLine("Choose a category:");

            var categories = categoryService.GetAll(p => p.Id < 100);
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }

            Console.Write("Enter category ID: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                Console.WriteLine("Invalid category ID.");
                return;
            }

            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.Write("Enter stock quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int stockQuantity))
            {
                Console.WriteLine("Invalid stock quantity.");
                return;
            }

            productService.Add(new ProductCreateDto
            {
                CategoryId = categoryId,
                Name = productName,
                Price = price,
                
            });

            // Display success message in green color
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Product added successfully!");
            Console.ResetColor(); // Reset to default color
        }

        static void UserMenu(UserDto user, Basket basket, IProductService productService)
        {
            Console.WriteLine("Available products:");
            Console.WriteLine(new string('-', 50));
            var products = productService.GetAll(p => p.Id < 100);

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name} - {product.CategoryName} - {product.Price}");
            }

            Console.WriteLine(new string('-', 50));
            Console.Write("Enter product ID to add to basket: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            var selectedProduct = productService.Get(p => p.Id == productId);
            if (selectedProduct == null)
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int productCount) || productCount <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            basket.UserId = user.Id;

            var existingItem = basket.BasketItems.FirstOrDefault(b => b.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.ProductCount += productCount;
            }
            else
            {
                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = productId,
                    ProductCount = productCount
                });
            }
            Console.WriteLine(new string('-', 50));

            // Display success message in green color
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" {productCount} units of {selectedProduct.Name} added to basket.");
            Console.ResetColor();
        }
    }
}

