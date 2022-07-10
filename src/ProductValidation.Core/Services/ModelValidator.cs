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

    public ValidationResult Validate<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        return modelValidator!.Validate(modelToValidate);
    }
    
    //TODO: investigate whether it is possible to avoid using await.  
    public async Task<ValidationResult> ValidateAsync<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        return await modelValidator!.ValidateAsync(modelToValidate);
    }

    private IValidator<T>? GetModelValidatorByType<T>()
    {
        return (IValidator<T>?)_serviceProvider.GetService(typeof(IValidator<T>));
    }
}