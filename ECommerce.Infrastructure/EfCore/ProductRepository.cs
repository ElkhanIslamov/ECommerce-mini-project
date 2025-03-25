using ECommerce.Application.DTOs;
using System.Linq.Expressions;
using System.Linq;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.EfCore;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
     public ProductRepository(AppDbContext context) : base(context)
    {

    }
   
}