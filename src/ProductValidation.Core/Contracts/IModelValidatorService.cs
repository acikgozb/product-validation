using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IModelValidatorService
{
    public List<FieldValidationResult> Validate<T>(T modelToValidate);
    
    public Task<List<FieldValidationResult>> ValidateAsync<T>(T modelToValidate);
}