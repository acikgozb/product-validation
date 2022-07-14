using OneOf;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Extensions;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductDataGateway _productDataGateway;
    private readonly IModelValidatorService _modelValidatorService;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork,
        IProductDataGateway productDataGateway, IModelValidatorService modelValidatorService)
    {
        _unitOfWork = unitOfWork;
        _productDataGateway = productDataGateway;
        _modelValidatorService = modelValidatorService;
    }

    /// <summary>
    /// Converts provided DTO to Product entity, validates it and asynchronously calls target data gateway to add Product to database. 
    /// </summary>
    /// <param name="productRequestDto">(<c>ProductRequestDto</c>) A product that is submitted by user.</param>
    /// <remarks>
    /// In this method, provided DTO is converted to Product entity and then business validations are ran against it.<br/>
    /// If any field fails its validation, a List of <c>FieldValidationResult</c> is returned back to the caller.
    /// If all fields pass, the method proceeds to call <c>ProductDataGateway</c> to add the validated Product.
    /// </remarks>
    /// <returns>
    /// List of <c>FieldValidationResult</c> - if any field is not valid.<br />
    /// <c>Product</c> - if fields are valid.
    /// </returns>
    public async Task<OneOf<List<FieldValidationResult>, Product>> AddProductAsync(ProductRequestDto productRequestDto)
    {
        var productEntity = productRequestDto.ToEntity();

        var validationResult = await _modelValidatorService.ValidateAsync(productEntity);
        if (validationResult.Count > 0)
        {
            return validationResult;
        }

        var addedProduct = _productDataGateway.AddProduct(productEntity);
        await _unitOfWork.SaveChangesAsync();

        return addedProduct;
    }

    /// <summary>
    /// Makes a call to target data gateway to get the list of all Product's. 
    /// </summary>
    /// <returns>
    /// A list of all Products. <br/>
    /// [] <c>List</c> - if there are no Products.
    /// </returns>
    public Task<List<Product>> GetProductsAsync()
    {
        return _productDataGateway.GetProductsAsync();
    }

    /// <summary>
    /// Makes a call to target data gateway to get the target Product by given Id. 
    /// </summary>
    /// <param name="Id">(<c>int</c>) Requested Product's Id.</param>
    /// <returns>
    /// <c>Product</c> - the corresponding Product for given Id.<br/>
    /// <c>null</c> - if there isn't any Product with given Id.
    /// </returns>
    public Task<Product?> GetProductByIdAsync(int id)
    {
        return _productDataGateway.GetProductByIdAsync(id);
    }
}