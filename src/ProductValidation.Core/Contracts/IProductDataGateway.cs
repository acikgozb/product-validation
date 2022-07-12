using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IProductDataGateway
{
    Task<bool> DoesProductExistByBarcodeAsync(string barcode);

    Product AddProduct(Product product);

    Task<List<Product>> GetProductsAsync();
}