using FluentValidation.Results;

namespace ProductValidation.Core.Contracts;

public interface IModelValidator
{
    public ValidationResult Validate<T>(T modelToValidate);
    
    public Task<ValidationResult> ValidateAsync<T>(T modelToValidate);
}