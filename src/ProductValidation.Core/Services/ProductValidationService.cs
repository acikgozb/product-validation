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

    /// <summary>
    /// Synchronously validates the given model and transforms any validation errors into a list of <c>FieldValidationResult</c>. 
    /// </summary>
    /// <param name="productModelToValidate">A model which has a defined <c>AbstractValidator</c>.</param>
    /// <remarks>
    /// This method only does synchronous validation, if there is a need for async validation, use <c>ValidateAsync()</c>.<br/>
    /// After validation, the method transforms each error into a <c>FieldValidationResult</c> and returns them in a <c>List</c>.
    /// </remarks>
    /// <returns>
    /// List of <c>FieldValidationResult</c> - if any field is not valid.<br />
    /// [] <c>List</c> - if all fields are valid.
    /// </returns>
    public List<FieldValidationResult> ValidateProduct<T>(T productModelToValidate)
    {
        var validationResult = _modelValidator.Validate(productModelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    /// <summary>
    /// Asynchronously validates the given model and transforms any validation errors into a list of <c>FieldValidationResult</c>. 
    /// </summary>
    /// <param name="productModelToValidate">A model which has a defined <c>AbstractValidator</c>.</param>
    /// <remarks>
    /// This method only does asynchronous validation, if there is a need for sync validation, use <c>Validate()</c>.<br/>
    /// After validation, the method transforms each error into a <c>FieldValidationResult</c> and returns them in a <c>List</c>.
    /// </remarks>
    /// <returns>
    /// List of <c>FieldValidationResult</c> - if any field is not valid.<br />
    /// [] <c>List</c> - if all fields are valid.
    /// </returns>
    public async Task<List<FieldValidationResult>> ValidateProductAsync<T>(T productModelToValidate)
    {
        var validationResult = await _modelValidator.ValidateAsync(productModelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    /// <summary>
    /// Transforms FluentValidation's <c>ValidationResult</c> type into <c>FieldValidationResult</c>. 
    /// </summary>
    /// <param name="validationResult">(<c>ValidationResult</c>) FluentValidation generated validation errors.</param>
    /// <returns>
    /// List of <c>FieldValidationResult</c> - if there are errors.<br />
    /// [] <c>List</c> - if there are no errors.
    /// </returns>
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