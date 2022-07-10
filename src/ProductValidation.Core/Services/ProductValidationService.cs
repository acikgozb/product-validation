using FluentValidation.Results;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Services;

public class ProductValidationService : IProductValidationService
{
    private readonly IModelValidator _modelValidator;

    public ProductValidationService(IModelValidator modelValidator)
    {
        _modelValidator = modelValidator;
    }

    public List<FieldValidationResult> ValidateProduct<T>(T productModelToValidate)
    {
        var validationResult = _modelValidator.Validate(productModelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    public async Task<List<FieldValidationResult>> ValidateProductAsync<T>(T productModelToValidate)
    {
        var validationResult = await _modelValidator.ValidateAsync(productModelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    private List<FieldValidationResult> ToFieldValidationResultList(ValidationResult validationResult)
    {
        return validationResult.Errors.ConvertAll(validationFailure =>
            new FieldValidationResult
            {
                FieldName = validationFailure.PropertyName,
                ErrorMessage = validationFailure.ErrorMessage
            });
    }
}