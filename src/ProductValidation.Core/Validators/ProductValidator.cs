using FluentValidation;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly IProductDataGateway _productDataGateway;
    private readonly IBarcodeLengthDataGateway _barcodeLengthDataGateway;
    private readonly IBannedWordsDataGateway _bannedWordsDataGateway;
    private const int DefaultRequiredBarcodeLength = 10;
    private string? _matchedBannedWord;
    private int _requiredBarcodeLength;

    public ProductValidator(IProductDataGateway productDataGateway, IBarcodeLengthDataGateway barcodeLengthDataGateway,
        IBannedWordsDataGateway bannedWordsDataGateway)
    {
        _productDataGateway = productDataGateway;
        _barcodeLengthDataGateway = barcodeLengthDataGateway;
        _bannedWordsDataGateway = bannedWordsDataGateway;
        RuleFor(p => p.Barcode)
            .MustAsync(IsBarcodeUnique)
            .WithMessage("A product with {PropertyValue} barcode already exists.")
            .MustAsync(DoesBarcodeLengthMatch)
            .WithMessage(product =>
                $"The entered barcode '{product.Barcode}' for brand '{product.Brand}' does not have required length. Its length should be {_requiredBarcodeLength} characters.");

        RuleFor(p => p.Description)
            .MustAsync(DoesBannedWordExist)
            .WithMessage(product =>
                $"The entered description contains a banned word '{_matchedBannedWord}', please change your product's description.");
    }

    private async Task<bool> IsBarcodeUnique(string? barcode, CancellationToken cancellationToken)
    {
        return !await _productDataGateway.DoesProductExistByBarcodeAsync(barcode!);
    }

    private async Task<bool> DoesBarcodeLengthMatch(Product product, string? barcode,
        CancellationToken cancellationToken)
    {
        var requiredLengthByBrand = await _barcodeLengthDataGateway.GetBarcodeLengthByBrandAsync(product.Brand!);
        _requiredBarcodeLength = requiredLengthByBrand?.Value ?? DefaultRequiredBarcodeLength;

        return barcode!.Length == _requiredBarcodeLength;
    }

    private async Task<bool> DoesBannedWordExist(Product product, string? description,
        CancellationToken cancellationToken)
    {
        var bannedWordsByBrand = await _bannedWordsDataGateway.GetBannedWordsByBrand(product.Brand!);
        if (bannedWordsByBrand is null)
        {
            return true;
        }

        _matchedBannedWord = bannedWordsByBrand
            .FirstOrDefault(bannedWord => description!.Contains(bannedWord.Value))?.Value;

        return string.IsNullOrEmpty(_matchedBannedWord);
    }
}