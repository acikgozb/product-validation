using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.Core.Extensions;

public static class ProductRequestDtoExtensions
{
    public static Product ToEntity(this ProductRequestDto productRequestDto)
    {
        return new Product
        {
            Name = productRequestDto.Name,
            Barcode = productRequestDto.Barcode,
            Brand = productRequestDto.Brand,
            Description = productRequestDto.Description
        };
    }
}