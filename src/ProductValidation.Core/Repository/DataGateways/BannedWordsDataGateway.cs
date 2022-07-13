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

    /// <summary>
    /// Generates a query to get a list of <c>BannedWords</c> based on the associated brand.
    /// </summary>
    /// <param name="Brand">(<c>string</c>)</param>
    /// <returns>
    /// BannedWord <c>List</c> - if the brand exists on DB and there are banned words linked to it.<br />
    /// [] <c>List</c> - if the associated brand does not exist on DB or if there are no banned words.
    /// </returns>
    public Task<List<BannedWord>?> GetBannedWordsByBrand(string brand)
    {
        return _dbContext.BannedWords
            .Where(word => word.Brand.Equals(brand))
            .GroupBy(word => word.Brand)
            .Select(wordGroup => wordGroup.ToList())
            .FirstOrDefaultAsync();
    }
}