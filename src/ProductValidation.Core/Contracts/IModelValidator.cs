namespace ProductValidation.Core.Contracts;

public interface IModelValidator
{
    public void Validate<T>(T modelToValidate);
}