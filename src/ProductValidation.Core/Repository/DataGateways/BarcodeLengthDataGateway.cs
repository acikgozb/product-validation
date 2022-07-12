using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository.DataGateways;

public class BarcodeLengthDataGateway : IBarcodeLengthDataGateway
{
    private readonly ProductValidationContext _dbContext;

    public BarcodeLengthDataGateway(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<BarcodeLength?> GetBarcodeLengthByBrandAsync(string brand)
    {
        return _dbContext.BarcodeLengths
            .FirstOrDefaultAsync(length => length.Brand.Equals(brand));
    }
}