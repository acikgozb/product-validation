using FluentValidation;
using FluentValidation.Results;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Exceptions;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Services;

public class ModelValidatorService : IModelValidatorService
{
    private readonly IServiceProvider _serviceProvider;

    public ModelValidatorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Synchronously validates the given model. 
    /// </summary>
    /// <param name="modelToValidate">A model which has a defined <c>AbstractValidator</c>.</param>
    /// <remarks>
    /// This method expects to get a type that has an <c>AbstractValidator</c> attached to it and injected to IoC container.<br/>
    /// This method only does synchronous validation, if there is a need for async validation, use <c>ValidateAsync()</c>.<br/>
    /// </remarks>
    /// <returns>
    /// <c>ValidationResult</c> - FluentValidation's default validation type - if any field is not valid.<br />
    /// [] <c>List</c> - if all fields are valid.
    /// </returns>
    public List<FieldValidationResult> Validate<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        ValidationResult validationResult = modelValidator!.Validate(modelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    /// <summary>
    /// Asynchronously validates the given model. 
    /// </summary>
    /// <param name="modelToValidate">A model which has a defined <c>AbstractValidator</c>.</param>
    /// <remarks>
    /// This method expects to get a type that has an <c>AbstractValidator</c> attached to it and injected to IoC container.<br/>
    /// This method only does asynchronous validation, if there is a need for sync validation, use <c>Validate()</c>.<br/>
    /// </remarks>
    /// <returns>
    /// <c>ValidationResult</c> - FluentValidation's default validation type - if any field is not valid.<br />
    /// [] <c>List</c> - if all fields are valid.
    /// </returns>
    //TODO: investigate whether it is possible to avoid using await.
    //TODO: Throw exception if model validator is null - this should not happen and if it happens that means someone forgot to inject.
    public async Task<List<FieldValidationResult>> ValidateAsync<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        if (modelValidator is null)
        {
            throw new NullValidatorException<T>();
        }

        ValidationResult validationResult = await modelValidator.ValidateAsync(modelToValidate);
        return ToFieldValidationResultList(validationResult);
    }

    /// <summary>
    /// Gets the attached <c>AbstractValidator</c> to the given type.
    /// </summary>
    /// <typeparam name="T">A type that is also used for its corresponding <c>AbstractValidator</c></typeparam>
    /// <returns>
    /// T <c>IValidator</c> - FluentValidation's generic validator interface.<br />
    /// <c>null</c> - if there is no <c>AbstractValidator</c> attached to given type.
    /// </returns>
    private IValidator<T>? GetModelValidatorByType<T>()
    {
        return (IValidator<T>?)_serviceProvider.GetService(typeof(IValidator<T>));
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