using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IBarcodeLengthDataGateway
{
    Task<BarcodeLength?> GetBarcodeLengthByBrandAsync(string brand);
}