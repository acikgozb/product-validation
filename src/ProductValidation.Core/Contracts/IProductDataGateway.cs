using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IProductDataGateway
{
    Task<bool> IsProductExistByBarcode(int barcode);
}