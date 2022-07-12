using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository;

public class ProductValidationContext : DbContext
{
    public ProductValidationContext(DbContextOptions<ProductValidationContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<BarcodeLength> BarcodeLengths { get; set; }

    public DbSet<BannedWord> BannedWords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BarcodeLength>().HasData(
            new BarcodeLength
            {
                Id = 1,
                Brand = "nike",
                Value = 12
            },
            new BarcodeLength
            {
                Id = 2,
                Brand = "adidas",
                Value = 15
            },
            new BarcodeLength
            {
                Id = 3,
                Brand = "apple",
                Value = 8
            }
        );

        modelBuilder.Entity<BannedWord>().HasData(
            new BannedWord
            {
                Id = 1,
                Brand = "nike",
                Value = "adidas"
            },
            new BannedWord
            {
                Id = 2,
                Brand = "nike",
                Value = "under armour"
            },
            new BannedWord
            {
                Id = 3,
                Brand = "nike",
                Value = "underarmour"
            },
            new BannedWord
            {
                Id = 4,
                Brand = "adidas",
                Value = "nike"
            },
            new BannedWord
            {
                Id = 5,
                Brand = "adidas",
                Value = "puma"
            },
            new BannedWord
            {
                Id = 6,
                Brand = "apple",
                Value = "google"
            },
            new BannedWord
            {
                Id = 7,
                Brand = "apple",
                Value = "microsoft"
            }
        );
    }
}