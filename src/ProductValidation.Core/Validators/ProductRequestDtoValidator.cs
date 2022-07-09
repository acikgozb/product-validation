using FluentValidation;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Validators;

public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("This field cannot be empty.")
            .MinimumLength(5)
            .WithMessage("The entered value '{PropertyValue}' must be at least {MinLength} characters.");

        RuleFor(dto => dto.Barcode)
            .NotEmpty()
            .WithMessage("This field cannot be empty.");
    }
}