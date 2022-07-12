using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository.DataGateways;

public class ProductDataGateway : IProductDataGateway
{
    private readonly ProductValidationContext _dbContext;

    public ProductDataGateway(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> DoesProductExistByBarcodeAsync(string barcode)
    {
        return _dbContext.Products.AnyAsync(product => product.Barcode == barcode);
    }

    public Product AddProduct(Product product)
    {
        var productEntry = _dbContext.Products.Add(product);
        return productEntry.Entity;
    }
}