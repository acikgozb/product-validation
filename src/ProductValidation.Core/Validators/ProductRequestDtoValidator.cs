using FluentValidation;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Validators;

//TODO acikgozb: Create extension methods for common validation rules below.
public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("This field cannot be empty.")
            .MinimumLength(5)
            .WithMessage("The entered value '{PropertyValue}' must be at least {MinLength} characters.")
            .Must(name => name?.All(char.IsLetter) ?? false)
            .WithMessage("This field can only contain letters.");

        RuleFor(dto => dto.Barcode)
            .NotEmpty()
            .WithMessage("This field cannot be empty.");

        RuleFor(dto => dto.Brand)
            .NotEmpty()
            .WithMessage("This field cannot be empty.")
            .MinimumLength(3)
            .WithMessage("The entered value '{PropertyValue}' must be at least {MinLength} characters.")
            .Must(brand => brand?.All(char.IsLetter) ?? false)
            .WithMessage("This field can only contain letters.");

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("This field cannot be empty");
    }
}