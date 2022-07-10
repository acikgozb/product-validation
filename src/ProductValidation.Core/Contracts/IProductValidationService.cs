using ProductValidation.Core.Models;

namespace ProductValidation.Core.Contracts;

public interface IProductValidationService
{
    public List<FieldValidationResult> ValidateProduct<T>(T productModelToValidate);

    public Task<List<FieldValidationResult>> ValidateProductAsync<T>(T productModelToValidate);
}