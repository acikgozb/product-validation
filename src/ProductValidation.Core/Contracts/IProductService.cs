using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Contracts;

public interface IProductService
{
    public Task AddProductAsync(ProductRequestDto productRequestDto);
}