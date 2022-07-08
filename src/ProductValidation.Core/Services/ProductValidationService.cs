using FluentValidation;
using ProductValidation.Core.Contracts;

namespace ProductValidation.Core.Services;

public class ProductValidationService : IModelValidator
{
    private readonly IServiceProvider _serviceProvider;

    public ProductValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Validate<T>(T modelToValidate)
    {
        var modelValidator = GetModelValidatorByType<T>();
        modelValidator!.Validate(modelToValidate);
    }
    
    public async Task ValidateAsync<T>(T modelToValidate)
    {
        //Call target validator, pass the data.
        //if succeeds, return nothing.
        //if not, throw exception. InvalidParameterException (?) -> maybe even handle it in more performant way.
        var modelValidator = GetModelValidatorByType<T>();
        await modelValidator!.ValidateAsync(modelToValidate);
    }

    private IValidator<T>? GetModelValidatorByType<T>()
    {
        return (IValidator<T>?)_serviceProvider.GetService(typeof(IValidator<T>));
    }
}