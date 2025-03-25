using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ECommerce.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppDbContext appDbContext = new AppDbContext();
            ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
            ICategoryService categoryService = new CategoryManager(categoryRepository);

            IUserRepository userRepository = new UserRepository(appDbContext);
            IUserService userService = new UserManager(userRepository);

            IProductRepository productRepository = new ProductRepository(appDbContext);
            IProductService productService = new ProductManager(productRepository);

            IOrderRepository orderRepository = new OrderRepository(appDbContext);
            IOrderService orderService = new OrderManager(orderRepository);

            Basket basket = new Basket();

            string username, password;

            Console.Write("Username:");
            username = Console.ReadLine();
            Console.Write("Password:");
            password = Console.ReadLine();

            var user = userService.Get(u => u.Username == username && u.Password == password);

            if (user.UserType == UserType.Admin)
            {
                Console.WriteLine("Choose one of commands");
                Console.WriteLine("Press one - To show all products");
                Console.WriteLine("Press two - To add  new product");
                int command = int.Parse(Console.ReadLine());

                if (command == 1)
                {
                    foreach (var item in productService.GetAll(p => p.Id < 100))
                    {
                        Console.WriteLine(item.Name);
                    };
                }
                else if (command == 2)
                {
                    Console.WriteLine("Please choose one of categories");

                    foreach (var item in categoryService.GetAll(p => p.Id < 100))
                    {
                        Console.WriteLine($"{item.Id} - {item.Name}");
                    };
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Add name :");
                    string name1 = Console.ReadLine();
                    Console.Write("Add price:");
                    decimal price = decimal.Parse(Console.ReadLine());

                    productService.Add(new ProductCreateDto
                    {
                        CategoryId = id,
                        Name = name1,
                        Price = price
                    });

                }
            }
            if (user.UserType == UserType.User)
            {
                Console.WriteLine("Here is our products");
                foreach (var product in productService.GetAll(p => p.Id < 100))
                {
                    Console.WriteLine($"{product.Id} {product.Name} {product.CategoryName} {product.Price}");
                };

                Console.WriteLine("Add to Basket press id number");
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine("Press 2 number for customer Id");
                int userId = int.Parse(Console.ReadLine());
                Console.WriteLine("Select number of count");
                int productCount = int.Parse(Console.ReadLine());
                
                basket.UserId = userId;
                basket.BasketItems.Add(new BasketItem 
                { 
                    ProductCount = productCount,
                    ProductId=productId 
                });

                         
                               

            }
        }
    }

}



