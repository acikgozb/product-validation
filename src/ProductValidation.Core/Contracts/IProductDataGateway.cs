namespace ProductValidation.Core.Contracts;

public interface IProductDataGateway
{
    Task<bool> IsProductExistByBarcode(string barcode);
}