using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository.DataGateways;

public class BannedWordsDataGateway : IBannedWordsDataGateway
{
    private readonly ProductValidationContext _dbContext;

    public BannedWordsDataGateway(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<BannedWord>?> GetBannedWordsByBrand(string brand)
    {
        return _dbContext.BannedWords
            .Where(word => word.Brand.Equals(brand))
            .GroupBy(word => word.Brand)
            .Select(wordGroup => wordGroup.ToList())
            .FirstOrDefaultAsync();
    }
}