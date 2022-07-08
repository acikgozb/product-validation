using FluentValidation;
using Microsoft.VisualBasic.CompilerServices;
using ProductValidation.Core.Dtos;

namespace ProductValidation.Core.Validators;

public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty()
            .MinimumLength(5);
    }
}