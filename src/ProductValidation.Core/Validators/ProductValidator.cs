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

    /// <summary>
    /// Makes a call to target data gateway to find out whether the given barcode is unique or not. 
    /// </summary>
    /// <param name="barcode">(<c>string?</c>)</param>
    /// <param name="cancellationToken">(<c>CancellationToken</c>) A token to notify whether the request is cancelled or not.</param>
    /// <remarks>
    /// The cancellation token logic is not implemented at this point.
    /// </remarks>
    /// <returns>
    /// <c>true</c> - if given barcode is truly unique.<br />
    /// <c>false</c> - if there is a product exist by given barcode.<br />
    /// </returns>
    private async Task<bool> IsBarcodeUnique(string? barcode, CancellationToken cancellationToken)
    {
        return !await _productDataGateway.DoesProductExistByBarcodeAsync(barcode!);
    }

    /// <summary>
    /// Makes a call to target data gateway to get required length, and validates it with given barcode.
    /// </summary>
    /// <param name="product">(<c>Product</c>) user-submitted Product.</param>
    /// <param name="barcode">(<c>string?</c>)</param>
    /// <param name="cancellationToken">(<c>CancellationToken</c>) A token to notify whether the request is cancelled or not.</param>
    /// <remarks>
    /// The cancellation token logic is not implemented at this point.
    /// If there is no length rule by provided brand, a default length rule (10) is applied to given barcode.
    /// </remarks>
    /// <returns>
    /// <c>true</c> - if given barcode length matches with required length.<br />
    /// <c>false</c> - if given barcode length does not match with required length.<br />
    /// </returns>
    private async Task<bool> DoesBarcodeLengthMatch(Product product, string? barcode,
        CancellationToken cancellationToken)
    {
        var requiredLengthByBrand = await _barcodeLengthDataGateway.GetBarcodeLengthByBrandAsync(product.Brand!);
        _requiredBarcodeLength = requiredLengthByBrand?.Value ?? DefaultRequiredBarcodeLength;

        return barcode!.Length == _requiredBarcodeLength;
    }

    /// <summary>
    /// Makes a call to target data gateway to get a list of banned words, and validates it with given description.
    /// </summary>
    /// <param name="product">(<c>Product</c>) user-submitted Product.</param>
    /// <param name="description">(<c>string?</c>)</param>
    /// <param name="cancellationToken">(<c>CancellationToken</c>) A token to notify whether the request is cancelled or not.</param>
    /// <remarks>
    /// The cancellation token logic is not implemented at this point.
    /// </remarks>
    /// <returns>
    /// <c>true</c> - if given description does not contain any banned word.<br />
    /// <c>false</c> - if given description contains at least one banned word.<br />
    /// </returns>
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