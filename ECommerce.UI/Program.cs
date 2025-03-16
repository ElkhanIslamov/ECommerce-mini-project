using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore;
using ECommerce.Infrastructure.EfCore.Context;

namespace ECommerce.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext appDbContext = new AppDbContext();
            ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
            ICategoryService categoryService = new CategoryManager(categoryRepository);

            categoryService.Add(new CategoryCreateDto { Name = "Fruit" });
            categoryService.Add(new CategoryCreateDto { Name = "Vegetables" });
        }
    }
}
