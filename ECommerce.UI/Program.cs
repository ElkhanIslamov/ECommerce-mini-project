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

            using (AppDbContext appDbContext = new AppDbContext(options))
            {
                ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
                ICategoryService categoryService = new CategoryManager(categoryRepository);

                IUserRepository userRepository = new UserRepository(appDbContext);
                IUserService userService = new UserManager(userRepository);

                IProductRepository productRepository = new ProductRepository(appDbContext);
                IProductService productService = new ProductManager(productRepository);

                IOrderRepository orderRepository = new OrderRepository(appDbContext);
                IOrderService orderService = new OrderManager(orderRepository);

                // Ensure Basket is initialized
                Basket basket = new Basket
                {
                    BasketItems = new List<BasketItem>() // Initialize BasketItems list
                };

                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                var user = userService.Get(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    Console.WriteLine("Invalid username or password. Exiting...");
                    return;
                }

                if (user.UserType == UserType.Admin)
                {
                    Console.WriteLine("Choose one of the commands:");
                    Console.WriteLine("1 - Show all products");
                    Console.WriteLine("2 - Add new product");

                    if (int.TryParse(Console.ReadLine(), out int command))
                    {
                        if (command == 1)
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
                                    Console.WriteLine(item.Name);
                                }
                            }
                        }
                        else if (command == 2)
                        {
                            Console.WriteLine("Choose a category:");

                            var categories = categoryService.GetAll(p => p.Id < 100);
                            foreach (var item in categories)
                            {
                                Console.WriteLine($"{item.Id} - {item.Name}");
                            }

                            Console.Write("Enter category ID: ");
                            if (int.TryParse(Console.ReadLine(), out int categoryId))
                            {
                                Console.Write("Enter product name: ");
                                string productName = Console.ReadLine();
                                Console.Write("Enter price: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                                {
                                    productService.Add(new ProductCreateDto
                                    {
                                        CategoryId = categoryId,
                                        Name = productName,
                                        Price = price
                                    });
                                    Console.WriteLine("Product added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid price.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid category ID.");
                            }
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
                else if (user.UserType == UserType.User)
                {
                    Console.WriteLine("Available products:");
                    var products = productService.GetAll(p => p.Id < 100);

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Id} - {product.Name} - {product.CategoryName} - {product.Price}");
                    }

                    Console.Write("Enter product ID to add to basket: ");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        var selectedProduct = productService.Get(p => p.Id == productId);
                        if (selectedProduct == null)
                        {
                            Console.WriteLine("Invalid product ID.");
                            return;
                        }

                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int productCount) && productCount > 0)
                        {
                            basket.UserId = user.Id;

                            // Ensure product is not already in the basket
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

                            Console.WriteLine($"{productCount} units of {selectedProduct.Name} added to basket.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID.");
                    }
                }
            }
        }
    }
}
