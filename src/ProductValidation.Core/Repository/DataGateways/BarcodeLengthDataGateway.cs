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

    /// <summary>
    /// Generates a query to get the required length of barcode based on its associated brand. 
    /// </summary>
    /// <param name="Brand">(<c>string</c>)</param>
    /// <returns>
    /// <c>BarcodeLength</c> - if rule is defined in database.<br />
    /// <c>null</c> - if there is no rule for associated brand.
    /// </returns>
    public Task<BarcodeLength?> GetBarcodeLengthByBrandAsync(string brand)
    {
        return _dbContext.BarcodeLengths
            .FirstOrDefaultAsync(length => length.Brand.Equals(brand));
    }
}