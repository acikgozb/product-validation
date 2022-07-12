using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IBannedWordsDataGateway
{
    Task<List<BannedWord>?> GetBannedWordsByBrand(string brand);
}