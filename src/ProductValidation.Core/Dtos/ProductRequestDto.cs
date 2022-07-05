namespace ProductValidation.Core.Dtos;

public class ProductRequestDto
{
    public string Name { get; set; }

    public string Brand { get; set; }

    public int Barcode { get; set; }

    public string Description { get; set; }
}