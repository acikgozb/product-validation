using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository;

public class ProductValidationContext : DbContext
{
    public ProductValidationContext(DbContextOptions<ProductValidationContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}