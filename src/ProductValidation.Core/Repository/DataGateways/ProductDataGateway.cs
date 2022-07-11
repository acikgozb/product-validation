using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Contracts;

namespace ProductValidation.Core.Repository.DataGateways;

public class ProductDataGateway : IProductDataGateway
{
    private readonly ProductValidationContext _dbContext;

    public ProductDataGateway(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsProductExistByBarcode(string barcode)
    {
        return _dbContext.Products.AnyAsync(product => product.Barcode == barcode);
    }
}