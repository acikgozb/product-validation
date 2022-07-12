using OneOf;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Extensions;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductDataGateway _productDataGateway;
    private readonly IProductValidationService _productValidationService;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductValidationService productValidationService, IUnitOfWork unitOfWork,
        IProductDataGateway productDataGateway)
    {
        _productValidationService = productValidationService;
        _unitOfWork = unitOfWork;
        _productDataGateway = productDataGateway;
    }

    public async Task<OneOf<List<FieldValidationResult>, Product>> AddProductAsync(ProductRequestDto productRequestDto)
    {
        var productEntity = productRequestDto.ToEntity();

        var validationResult = await _productValidationService.ValidateProductAsync(productEntity);
        if (validationResult.Count > 0)
        {
            return validationResult;
        }

        var addedProduct = _productDataGateway.AddProduct(productEntity);
        await _unitOfWork.SaveChangesAsync();

        return addedProduct;
    }

    public Task<List<Product>> GetProductsAsync()
    {
        return _productDataGateway.GetProductsAsync();
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        return _productDataGateway.GetProductByIdAsync(id);
    }
}