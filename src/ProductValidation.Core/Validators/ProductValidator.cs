using FluentValidation;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator(IProductDataGateway productDataGateway)
    {
        RuleFor(p => p.Barcode)
            .MustAsync(async (barcode, cancellationToken) => !await productDataGateway.IsProductExistByBarcode(barcode!.Value))
            .WithMessage("A product with {PropertyValue} barcode already exists.");
    }
}