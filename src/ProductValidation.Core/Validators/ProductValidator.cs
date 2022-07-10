using FluentValidation;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        //Contains business model validations
        RuleFor(p => p.Barcode)
            .MustAsync(async (barcode, cancellationToken) =>
            {
                // TODO acikgozb: check if product exists for given barcode.
                return true;
            })
            .WithMessage("A product with {PropertyValue} already exists.");
    }
}