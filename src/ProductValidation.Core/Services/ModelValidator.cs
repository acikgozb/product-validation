using FluentValidation;
using FluentValidation.Results;
using ProductValidation.Core.Contracts;

namespace ProductValidation.Core.Services;

public class ModelValidator : IModelValidator
{
    private readonly IServiceProvider _serviceProvider;

    public ModelValidator(IServiceProvider serviceProvider)
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
    /// <c>ValidationResult</c> - FluentValidation's default validation type.<br />
    /// </returns>
    public ValidationResult Validate<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        return modelValidator!.Validate(modelToValidate);
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
    /// <c>ValidationResult</c> - FluentValidation's default validation type.<br />
    /// </returns>
    //TODO: investigate whether it is possible to avoid using await.
    public async Task<ValidationResult> ValidateAsync<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        return await modelValidator!.ValidateAsync(modelToValidate);
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
}