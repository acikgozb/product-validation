namespace ProductValidation.Core.Models.Dtos;

public class ProductRequestDto
{
    public string? Name { get; set; }

    public string? Brand { get; set; }

    public string? Barcode { get; set; }

    public string? Description { get; set; }
}