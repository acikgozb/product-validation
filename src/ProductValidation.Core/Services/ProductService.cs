using ProductValidation.Core.Contracts;
using ProductValidation.Core.Extensions;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductValidationService _productValidationService;

    public ProductService(IProductValidationService productValidationService)
    {
        _productValidationService = productValidationService;
    }

    public async Task AddProductAsync(ProductRequestDto productRequestDto)
    {
        //map to entity, then validate the entity.
        var productEntity = productRequestDto.ToEntity();
        var validationResult = await _productValidationService.ValidateProductAsync(productEntity);
        throw new NotImplementedException();
    }
}