using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions
{
    public static class ProductMapExtensions
    {
        public static Product ToProduct(this ProductCreateDto createDto)
        {
            return new Product
            {
                Name = createDto.Name
                
            };
        }
        public static Product ToProductDto(this ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
            };
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                
                               
            };
        }

        public static ProductCreateDto ToProductCreateDto(this Product product)
        {
            return new ProductCreateDto
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
        }

        public static Product ToProductCreateDto(this ProductCreateDto productCreateDto)
        {
            return new Product
            {
               Price = productCreateDto.Price,
               CategoryId = productCreateDto.CategoryId,
               Name = productCreateDto.Name,
            };
        }

    }
}
