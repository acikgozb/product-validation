using OneOf;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Contracts;

public interface IProductService
{
    public Task<OneOf<List<FieldValidationResult>, Product>> AddProductAsync(ProductRequestDto productRequestDto);

    public Task<List<Product>> GetProductsAsync();

    public Task<Product?> GetProductByIdAsync(int id);
}